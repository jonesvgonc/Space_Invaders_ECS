using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EnemyWaveCreationSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<WaveFlag>();
    }

    protected override void OnUpdate()
    {
        for (int indexX = 0; indexX < 11; indexX++)
        {
            for (int indexY = 0; indexY < 5; indexY++)
            {
                float FirstXPosition = -2.5f;

                switch (indexY)
                {
                    case 0:
                        var enemy1 = EntityManager.Instantiate(GameObjectsManager.Enemy1Entity);
                        EntityManager.SetComponentData(enemy1, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        break;
                    case 1:
                        var enemy2 = EntityManager.Instantiate(GameObjectsManager.Enemy2Entity);
                        EntityManager.SetComponentData(enemy2, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        break;
                    case 2:
                        var enemy3 = EntityManager.Instantiate(GameObjectsManager.Enemy3Entity);
                        EntityManager.SetComponentData(enemy3, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        break;
                    case 3:
                        var enemy4 = EntityManager.Instantiate(GameObjectsManager.Enemy4Entity);
                        EntityManager.SetComponentData(enemy4, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        break;
                    case 4:
                        var enemy5 = EntityManager.Instantiate(GameObjectsManager.Enemy5Entity);
                        EntityManager.SetComponentData(enemy5, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        break;
                    default:
                        break;
                }

            }
        }

        EntityManager.DestroyEntity(GetSingletonEntity<WaveFlag>());
    }
}
