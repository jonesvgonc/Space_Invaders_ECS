using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class UIAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public bool IsMainMenu = false;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new UIComponent() { IsMainMenu = IsMainMenu});
        if(!IsMainMenu)
            dstManager.AddComponentData(entity, new UIInGameFlag() {});
    }
}
