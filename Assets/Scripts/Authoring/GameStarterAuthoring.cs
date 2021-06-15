using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameStarterAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<CanStartGameFlag>(entity);
    }
}
