using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class StartGameSystem : ComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<NewGameFlag>();
    }

    protected override void OnUpdate()
    {
        var inGameEntity = EntityManager.CreateEntity();
        EntityManager.AddComponentData(inGameEntity, new InGameFlag());

        var player = EntityManager.Instantiate(GameObjectsManager.PlayerEntity);
        
        EntityManager.SetComponentData(player, new Translation
        {
            Value = new float3(0, -3.5f, 0)
        });

        MountBarriers();

        var wave = EntityManager.CreateEntity();
        var gameData = EntityManager.CreateEntity();
        EntityManager.AddComponentData(gameData, new GameDataComponent()
        {
            EnemyQuantity = 45,
            Lives = 3,
            Score = 0,
            Level = 1
        });

        EntityManager.AddComponentData(wave, new WaveFlag());

        EntityManager.DestroyEntity(GetSingletonEntity<NewGameFlag>());
    }

    private void MountBarriers()
    {
        float FirstXPosition = -4;

        for(int index = 0; index < 3; index++)
        {
            for(int indexX = 0; indexX < 7; indexX ++)
            {
                for (int indexY = 0; indexY < 5; indexY++)
                {
                    var block = EntityManager.Instantiate(GameObjectsManager.BlockBarrierEntity);
                    EntityManager.SetComponentData(block, new Translation
                    {
                        Value = new float3((indexX * 0.15f) + FirstXPosition, (indexY * 0.15f) - 2, 0)
                    });
                }
            }
            FirstXPosition += 3.5f;
        }
    }
}
