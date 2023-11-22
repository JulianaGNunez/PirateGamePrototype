using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MovePositionAStar : MonoBehaviour
{
    public AILerp _aiLerp;

    public Transform _target;

    public void Update()
    {
        _aiLerp.destination = _target.position;
    }
}
