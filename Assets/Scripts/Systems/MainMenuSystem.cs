using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class MainMenuSystem : SystemBase
{

    private bool startingScene = true;

    protected override void OnCreate()
    {
        base.OnCreate();

        RequireSingletonForUpdate<CanStartGameFlag>();
    }

    protected override void OnUpdate()
    {
        if(startingScene)
        {
            startingScene = false;

            Entities
                .ForEach((Entity entity,
                in UIComponent uiComponent) =>
                {
                    if (uiComponent.IsMainMenu)
                        EntityManager.GetComponentObject<UIEnableDisable>(entity).Enable();
                    else
                        EntityManager.GetComponentObject<UIEnableDisable>(entity).Disable();
                })
                .WithoutBurst()
                .Run();
        }

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

            var newEntity = EntityManager.CreateEntity();

            EntityManager.AddComponent<NewGameFlag>(newEntity);
        }
    }
}
