using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct GameDataComponent : IComponentData
{
    public int EnemyQuantity;
    public int Score;
    public int Lives;
}
