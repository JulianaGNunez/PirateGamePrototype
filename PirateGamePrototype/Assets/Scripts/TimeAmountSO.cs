using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeAmountSO", menuName = "ScriptableObjects/TimeAmountSO")]
public class TimeAmountSO : ScriptableObject
{
    public float _timeAmount = 1.5f;
    public float _timeAmountSpawnEmenies = 10f;
}
