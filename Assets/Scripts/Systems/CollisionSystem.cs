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

                        var collide = math.distance(translation.Value, enemyTranslation.Value) <= .5f;

                        if (collide)
                        {                            
                            EntityManager.AddComponentData(attackEntity, new DestroyFlag());
                            EntityManager.AddComponentData(enemiesEntities[i], new DestroyFlag());
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

                        var collide = math.distance(translation.Value, barrierTranslation.Value) <= .5f;

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

    }
}
