using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameObjectsManager : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject PlayerPrefab;
    public static Entity PlayerEntity;

    public GameObject Enemy1Prefab;
    public static Entity Enemy1Entity;

    public GameObject Enemy2Prefab;
    public static Entity Enemy2Entity;

    public GameObject Enemy3Prefab;
    public static Entity Enemy3Entity;

    public GameObject Enemy4Prefab;
    public static Entity Enemy4Entity;

    public GameObject Enemy5Prefab;
    public static Entity Enemy5Entity;
       

    public GameObject SpecialEnemyPrefab;
    public static Entity SpecialEnemyEntity;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        PlayerEntity = conversionSystem.GetPrimaryEntity(PlayerPrefab);
        Enemy1Entity = conversionSystem.GetPrimaryEntity(Enemy1Prefab);
        Enemy2Entity = conversionSystem.GetPrimaryEntity(Enemy2Prefab);
        Enemy3Entity = conversionSystem.GetPrimaryEntity(Enemy3Prefab);
        Enemy4Entity = conversionSystem.GetPrimaryEntity(Enemy4Prefab);
        Enemy5Entity = conversionSystem.GetPrimaryEntity(Enemy5Prefab);       
        SpecialEnemyEntity = conversionSystem.GetPrimaryEntity(SpecialEnemyPrefab);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(PlayerPrefab);

        referencedPrefabs.Add(Enemy1Prefab);
        referencedPrefabs.Add(Enemy2Prefab);
        referencedPrefabs.Add(Enemy3Prefab);
        referencedPrefabs.Add(Enemy4Prefab);
        referencedPrefabs.Add(Enemy5Prefab);

        referencedPrefabs.Add(SpecialEnemyPrefab);
    }
}
