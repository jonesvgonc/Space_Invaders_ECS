using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float PlayerSpeed = 6f;
    public float3 MoveDirection;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerComponent() { CharacterSpeed = PlayerSpeed});
        dstManager.AddBuffer<MovementDataComponent>(entity);
        dstManager.AddBuffer<AttackBufferComponent>(entity);
        dstManager.AddComponentData(entity, new MoveDirectionComponent
        {
            MoveDirection = MoveDirection
        });
    }
}
