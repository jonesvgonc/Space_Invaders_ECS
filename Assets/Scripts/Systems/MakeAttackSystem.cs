using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public class MakeAttackSystem : SystemBase
{

    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {

        Entities
               .ForEach((Entity entity, MakeAttackFlag attack) =>
               {
                   var attackEntity = EntityManager.Instantiate(GameObjectsManager.BlockBarrierEntity);

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

                   EntityManager.DestroyEntity(entity);
               })
               .WithStructuralChanges()
               .WithoutBurst()
               .Run();
    }
}
