using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public class CollisionSystem : SystemBase
{
    private EntityQuery _enemyQuery;
    private EntityQuery _blockQuery;
    private EntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();

        _enemyQuery = GetEntityQuery(typeof(EnemyComponent));
        _blockQuery = GetEntityQuery(typeof(BlockBarrierComponent));

        _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        var enemiesEntities = _enemyQuery.ToEntityArray(Allocator.TempJob);
        var commandBuffer = _commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

        if (enemiesEntities.Length > 0)
        {
            Entities
                .ForEach((Entity attackEntity,
                ref Translation translation,
                ref AttackComponent attackData) =>
                {
                    for (int i = 0; i < enemiesEntities.Length; i++)
                    {
                        var enemyTranslation = EntityManager.GetComponentData<Translation>(enemiesEntities[i]);

                        var collide = math.distance(translation.Value, enemyTranslation.Value) <= .5f && attackData.IsPlayerAttack;

                        if (collide)
                        {
                            EntityManager.AddComponentData(attackEntity, new DestroyFlag());
                            EntityManager.AddComponentData(enemiesEntities[i], new DestroyFlag());
                            var gameData = GetSingleton<GameDataComponent>();
                            if(!EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]).isSpecialEnemy)
                                gameData.EnemyQuantity--;
                            if(gameData.EnemyQuantity < 1)
                            {
                                var ent = EntityManager.CreateEntity();
                                EntityManager.AddComponentData(ent, new WaveFlag() { isSpecialEnemy = false }); 
                                gameData.Level++;
                            }
                            gameData.Score += EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]).Score;
                            SetSingleton<GameDataComponent>(gameData);
                        }
                    }
                })
                .WithStructuralChanges()
                .WithoutBurst()
                .Run();
        }

        enemiesEntities.Dispose();

        var barrierBlockEntities = _blockQuery.ToEntityArray(Allocator.TempJob);

        if (barrierBlockEntities.Length > 0)
        {
            Entities
                .ForEach((Entity attackEntity,
                ref Translation translation,
                ref AttackComponent attackData) =>
                {
                    for (int i = 0; i < barrierBlockEntities.Length; i++)
                    {
                        var barrierTranslation = EntityManager.GetComponentData<Translation>(barrierBlockEntities[i]);

                        var collide = math.distance(translation.Value, barrierTranslation.Value) <= .1f;

                        if (collide)
                        {
                            EntityManager.AddComponentData(attackEntity, new DestroyFlag());
                            EntityManager.AddComponentData(barrierBlockEntities[i], new DestroyFlag());
                        }
                    }
                })
                .WithStructuralChanges()
                .WithoutBurst()
                .Run();
        }

        barrierBlockEntities.Dispose();

        Entities
                  .ForEach((Entity attackEntity,
                  ref Translation translation,
                  ref AttackComponent attackData) =>
                  {
                      var playerTranslation = EntityManager.GetComponentData<Translation>(GetSingletonEntity<PlayerComponent>());

                      var collide = math.distance(translation.Value, playerTranslation.Value) <= .5f && !attackData.IsPlayerAttack;

                      if (collide)
                      {
                          EntityManager.AddComponentData(attackEntity, new DestroyFlag());
                          var gameData = GetSingleton<GameDataComponent>();
                          gameData.Lives -= 1;
                          if(gameData.Lives >= 0)
                              EntityManager.GetComponentObject<InGameUIUpdates>(GetSingletonEntity<UIInGameFlag>()).RemoveLife();
                          else
                          {
                              var ent = EntityManager.CreateEntity();
                              EntityManager.AddComponent<GameOverFlag>(ent);
                          }

                          SetSingleton<GameDataComponent>(gameData);
                      }
                  })
                  .WithStructuralChanges()
                  .WithoutBurst()
                  .Run();

    }
}
