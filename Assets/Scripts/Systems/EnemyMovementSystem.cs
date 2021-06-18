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

    protected override void OnCreate()
    {
        base.OnCreate();

    }

    protected override void OnUpdate()
    {
        var point = 0f;
        Entities
               .ForEach((EnemyComponent enemy, Translation translation) =>
               {
                   if (translation.Value.x < -8)
                   {
                       rightSide = true;
                       point = -15f;
                   }
                   if (translation.Value.x > 8)
                   {
                       rightSide = false;
                       point = -15f;
                   }
               })
               .WithoutBurst()
               .Run();

        var side = new float3(1, point, 0);

        if (!rightSide) side = new float3(-1, point, 0);

       Entities
                .ForEach((EnemyComponent enemy, DynamicBuffer<MovementDataComponent> moveData) =>
                {
                    moveData.Add(new MovementDataComponent
                    {
                        Position = side
                    });
                })
                .Schedule();
    }
}
