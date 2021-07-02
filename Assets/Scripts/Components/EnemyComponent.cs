using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct EnemyComponent : IComponentData
{
    public float EnemySpeed;
    public int EnemyRow;
    public int Score;
    public bool isSpecialEnemy;
    public float LifeTime;
}
