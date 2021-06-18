using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float EnemySpeed = 1f;
    public float3 MoveDirection;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new EnemyComponent() { EnemySpeed =EnemySpeed });
        dstManager.AddBuffer<MovementDataComponent>(entity);
        dstManager.AddComponentData(entity, new MoveDirectionComponent
        {
            MoveDirection = MoveDirection
        });
    }
}
