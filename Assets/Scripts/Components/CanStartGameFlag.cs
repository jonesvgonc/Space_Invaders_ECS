using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct CanStartGameFlag : IComponentData 
{
    public bool StartingScene;
}
