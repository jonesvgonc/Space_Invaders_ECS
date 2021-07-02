using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class GameOverSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<GameOverFlag>();
    }

    protected override void OnUpdate()
    {
        Entities
           .ForEach((Entity entity, GameObjectFlag flag) =>
           {
               EntityManager.AddComponent<DestroyFlag>(entity);
           })
           .WithStructuralChanges()
           .WithoutBurst()
           .Run();

        var ent = EntityManager.CreateEntity();
        EntityManager.AddComponentData(ent, new CanStartGameFlag() { StartingScene = true });

        EntityManager.DestroyEntity(GetSingletonEntity<GameDataComponent>());
        EntityManager.DestroyEntity(GetSingletonEntity<GameOverFlag>());
        EntityManager.DestroyEntity(GetSingletonEntity<InGameFlag>());
    }
}
