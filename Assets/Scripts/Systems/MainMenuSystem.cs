using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class MainMenuSystem : SystemBase
{

    protected override void OnCreate()
    {
        base.OnCreate();

        RequireSingletonForUpdate<CanStartGameFlag>();
    }

    protected override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EntityManager.DestroyEntity(GetSingletonEntity<CanStartGameFlag>());            

            Entities
                .ForEach((Entity entity,                
                in UIComponent uiComponent) =>
                {
                    if(uiComponent.IsMainMenu)
                        EntityManager.GetComponentObject<UIEnableDisable>(entity).Disable();
                    else
                        EntityManager.GetComponentObject<UIEnableDisable>(entity).Enable();
                })
                .WithoutBurst()
                .Run();
        }
    }
}
