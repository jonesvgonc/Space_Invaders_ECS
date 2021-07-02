using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BlockBarrierAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BlockBarrierComponent());
        dstManager.AddComponent<GameObjectFlag>(entity);
    }
}
