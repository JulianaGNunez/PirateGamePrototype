using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayRotation : MonoBehaviour
{
    public Transform _target;

    // Update is called once per frame
    void Update()
    {
        _target.eulerAngles = Vector3.zero;
    }
}
