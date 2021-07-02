using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class UIControleSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        
        RequireSingletonForUpdate<InGameFlag>();
    }
    protected override void OnUpdate()
    {
        var componentUI = GetSingletonEntity<UIInGameFlag>();
        EntityManager.GetComponentObject<InGameUIUpdates>(componentUI).SetScore(GetSingleton<GameDataComponent>().Score);
    }
}
