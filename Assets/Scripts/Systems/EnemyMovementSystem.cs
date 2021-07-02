using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class EnemyMovementSystem : SystemBase
{
    bool rightSide = false;

    float timeToSpecialEnemy = 2;
    float specialEnemyLifeTime = 0;

    protected override void OnCreate()
    {
        base.OnCreate();

        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        timeToSpecialEnemy -= Time.DeltaTime;
        specialEnemyLifeTime += Time.DeltaTime;
        if (timeToSpecialEnemy < 0)
        {
            var wave = EntityManager.CreateEntity();
            specialEnemyLifeTime = 0;
            EntityManager.AddComponentData(wave, new WaveFlag() { isSpecialEnemy = true });
            timeToSpecialEnemy = 20;
        }
        
        var point = 0f;
        Entities
               .ForEach((Entity entity, EnemyComponent enemy, Translation translation) =>
               {
                   if (translation.Value.x < -8 && !enemy.isSpecialEnemy)
                   {
                       rightSide = true;
                       point = -15f;
                   }
                   if (translation.Value.x > 8 && !enemy.isSpecialEnemy)
                   {
                       rightSide = false;
                       point = -15f;
                   }
                   if (specialEnemyLifeTime > 8 && enemy.isSpecialEnemy)
                   {
                       EntityManager.AddComponent<DestroyFlag>(entity);
                   }
               })
               .WithStructuralChanges()
               .WithoutBurst()
               .Run();

        var side = new float3(1, point, 0);

        if (!rightSide) side = new float3(-1, point, 0);

        var specialEnemySide = new float3(-1, 0, 0);

        Entities
                 .ForEach((EnemyComponent enemy, DynamicBuffer<MovementDataComponent> moveData) =>
                 {
                     if (enemy.isSpecialEnemy)
                     {
                         moveData.Add(new MovementDataComponent
                         {
                             Position = specialEnemySide
                         });
                     }
                     else
                         moveData.Add(new MovementDataComponent
                         {
                             Position = side
                         });
                 })
                 .Schedule();
    }
}
