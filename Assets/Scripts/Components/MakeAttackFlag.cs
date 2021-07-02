using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct MakeAttackFlag : IComponentData
{
    public bool playerAttack;
}

public struct GameOverFlag : IComponentData
{
}

