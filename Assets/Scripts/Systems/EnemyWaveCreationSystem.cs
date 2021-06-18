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
                        var enemyComp = EntityManager.GetComponentData<EnemyComponent>(enemy1);
                        enemyComp.EnemyRow = indexY;
                        EntityManager.SetComponentData(enemy1, enemyComp);

                        break;
                    case 1:
                        var enemy2 = EntityManager.Instantiate(GameObjectsManager.Enemy2Entity);
                        EntityManager.SetComponentData(enemy2, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        var enemyComp2 = EntityManager.GetComponentData<EnemyComponent>(enemy2);
                        enemyComp2.EnemyRow = indexY;
                        EntityManager.SetComponentData(enemy2, enemyComp2);
                        break;
                    case 2:
                        var enemy3 = EntityManager.Instantiate(GameObjectsManager.Enemy3Entity);
                        EntityManager.SetComponentData(enemy3, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        var enemyComp3 = EntityManager.GetComponentData<EnemyComponent>(enemy3);
                        enemyComp3.EnemyRow = indexY;
                        EntityManager.SetComponentData(enemy3, enemyComp3);
                        break;
                    case 3:
                        var enemy4 = EntityManager.Instantiate(GameObjectsManager.Enemy4Entity);
                        EntityManager.SetComponentData(enemy4, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        var enemyComp4 = EntityManager.GetComponentData<EnemyComponent>(enemy4);
                        enemyComp4.EnemyRow = indexY;
                        EntityManager.SetComponentData(enemy4, enemyComp4);
                        break;
                    case 4:
                        var enemy5 = EntityManager.Instantiate(GameObjectsManager.Enemy5Entity);
                        EntityManager.SetComponentData(enemy5, new Translation
                        {
                            Value = new float3((indexX * 1f) + FirstXPosition, (indexY * 0.75f) + 1.5f, 0)
                        });
                        var enemyComp5 = EntityManager.GetComponentData<EnemyComponent>(enemy5);
                        enemyComp5.EnemyRow = indexY;
                        EntityManager.SetComponentData(enemy5, enemyComp5);
                        break;
                    default:
                        break;
                }
            }
        }

        EntityManager.DestroyEntity(GetSingletonEntity<WaveFlag>());
    }
}
