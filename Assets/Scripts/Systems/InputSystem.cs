using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class InputSystem : SystemBase
{
    private float timingAttack = 2f;
    private float attackTimeCooldown = 1f;

    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        timingAttack += Time.DeltaTime;
        var attackShot = Input.GetKeyDown(KeyCode.Space);
        var tPosition = float3.zero;
        tPosition = new float3(Input.GetAxisRaw("Horizontal"), 0, 0);

        Entities
                .ForEach((PlayerComponent player, DynamicBuffer<MovementDataComponent> moveData) =>
                {
                    moveData.Add(new MovementDataComponent
                    {
                        Position = tPosition
                    });
                })
                .Schedule();

        if (attackShot && timingAttack > attackTimeCooldown)
        {
            timingAttack = 0f;
            Entities
                .ForEach((PlayerComponent player, DynamicBuffer<AttackBufferComponent> attackBuffer) =>
                {
                    attackBuffer.Add(new AttackBufferComponent());
                })
                .Schedule();
        }
    }
}
