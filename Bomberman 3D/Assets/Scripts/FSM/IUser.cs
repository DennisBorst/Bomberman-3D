using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IUser
{
    Transform transform { get; }
    NavMeshAgent navMeshAgent { get; }

    float rayCastLength { get; }
    RaycastHit[] hit { get; }
    Vector3[] directions { get; }
    Vector3 forward { get; }

    Bomb bomb { get; }

    void DeployBomb();
}
