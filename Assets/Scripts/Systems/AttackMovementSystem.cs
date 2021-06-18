using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public class AttackMovementSystem : SystemBase
{
    private EntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
        _commandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();

        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        var delta = Time.DeltaTime;
        var commandBuffer = _commandBufferSystem.CreateCommandBuffer().AsParallelWriter();
        Dependency = Entities
            .ForEach((int entityInQueryIndex, Entity entity, Rotation rotation, ref AttackComponent projectile, ref Translation translation) =>
            {
                if (projectile.IsPlayerAttack)
                {
                    var newTranslation = new float3(0, 1, 0) * 10 * delta;
                    translation.Value += newTranslation;
                }else
                {
                    var newTranslation = new float3(0, -1, 0) * 10 * delta;
                    translation.Value += newTranslation;
                }
                projectile.LifeTime += delta;
                if (projectile.LifeTime > 1.5f)
                    commandBuffer.AddComponent<DestroyFlag>(entityInQueryIndex, entity);
            })
            .ScheduleParallel(Dependency);
    }
}
