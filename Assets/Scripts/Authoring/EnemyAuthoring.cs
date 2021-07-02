using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float EnemySpeed = 1f;
    public float3 MoveDirection;
    public int Score;
    public bool isSpecialEnemy = false;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new EnemyComponent() { EnemySpeed =EnemySpeed, Score = Score, isSpecialEnemy = isSpecialEnemy });
        dstManager.AddBuffer<MovementDataComponent>(entity);
        dstManager.AddComponentData(entity, new MoveDirectionComponent
        {
            MoveDirection = MoveDirection
            
        });
        dstManager.AddComponent<GameObjectFlag>(entity);
    }
}
