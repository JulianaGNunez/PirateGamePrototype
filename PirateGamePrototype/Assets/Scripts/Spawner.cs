using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public TMP_Text _timerText;

    public List<Transform> _spawnPoints;

    [HideInInspector]
    public bool _timerOn = false;

    public bool _enableSpawnCourotine = false;

    private float _timer;

    private void Awake()
    {
        _timer = 2f;
    }

    public void StartSpawnCourotine()
    {
        _enableSpawnCourotine = true;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    private IEnumerator SpawnCourotine()
    {
        yield return null;
    }
}
