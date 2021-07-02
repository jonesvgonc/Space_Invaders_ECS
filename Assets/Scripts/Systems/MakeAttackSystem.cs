using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public class MakeAttackSystem : SystemBase
{
    private Random _random;
    private EntityQuery _enemyQuery;

    protected override void OnCreate()
    {
        base.OnCreate();
        _random = new Random(0xd3425a);
        _enemyQuery = GetEntityQuery(typeof(EnemyComponent));
        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        var random = new Random(_random.NextUInt());
        var enemiesEntities = _enemyQuery.ToEntityArray(Allocator.TempJob);
        var enemyIndex = random.NextInt(0, enemiesEntities.Length - 1);

        Entities
               .ForEach((Entity entity, MakeAttackFlag attack) =>
               {
                   var attackEntity = EntityManager.Instantiate(GameObjectsManager.AttackEntity);

                   if (attack.playerAttack)
                   {
                       var translationPlayer = EntityManager.GetComponentData<Translation>(GetSingletonEntity<PlayerComponent>());

                       EntityManager.SetComponentData(attackEntity, new Translation
                       {
                           Value = translationPlayer.Value
                       });

                       EntityManager.AddComponentData(attackEntity, new AttackComponent
                       {
                           IsPlayerAttack = true,
                           LifeTime = 0
                       });
                   }
                   else
                   {
                       var translationEnemy = EntityManager.GetComponentData<Translation>(enemiesEntities[enemyIndex]);
                       EntityManager.SetComponentData(attackEntity, new Translation
                       {
                           Value = translationEnemy.Value
                       });
                       EntityManager.AddComponentData(attackEntity, new AttackComponent
                       {
                           IsPlayerAttack = false,
                           LifeTime = 0
                       });
                   }

                   EntityManager.DestroyEntity(entity);
               })
               .WithStructuralChanges()
               .WithoutBurst()
               .Run();

        enemiesEntities.Dispose();
    }
}
