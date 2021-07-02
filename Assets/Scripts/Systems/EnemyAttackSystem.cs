using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


[UpdateInGroup(typeof(InitializationSystemGroup))]

public class EnemyAttackSystem : SystemBase
{    
    private float timingAttack = 0f;
    private float attackTimeCooldown = 0.75f;

    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        timingAttack += Time.DeltaTime;
        if (timingAttack > attackTimeCooldown)
        {
            timingAttack = 0f;
            var attEntity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(attEntity, new MakeAttackFlag() { playerAttack = false });
        }
    }
}
