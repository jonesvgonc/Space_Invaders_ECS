using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public class MovementSystem : SystemBase
{
    private EntityQuery _query;

    protected override void OnCreate()
    {
        base.OnCreate();
        _query = GetEntityQuery(typeof(MoveDirectionComponent), ComponentType.ReadOnly<Rotation>(), ComponentType.ReadOnly<MovementDataComponent>());
        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        var entities = _query.ToEntityArray(Allocator.TempJob);

        for (int i = 0; i < entities.Length; i++)
        {
            var buffer = EntityManager.GetBuffer<MovementDataComponent>(entities[i]);

            float3 directionSum = 0;
            for (int j = 0; j < buffer.Length; j++)
            {
                directionSum += buffer[j].Position;
            }

            var characterDirection = math.normalizesafe(directionSum);
            EntityManager.SetComponentData(entities[i], new MoveDirectionComponent
            {
                MoveDirection = characterDirection
            });

            buffer.Clear();
        }
        entities.Dispose();
    }
}
