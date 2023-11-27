using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Spawner : MonoBehaviour
{
    public TMP_Text _timerText;

    public List<Transform> _spawnPoints;

    public List<GameObject> _enemyPrefabs;

    public TimeAmountSO _timeAmountSO;

    public ShipLife _playerTarget;

    private List<GameObject> _spawnedObjects;

    public UnityEvent _endedGameSucessful;

    [HideInInspector]
    public int _playerPoints = 0;

    [HideInInspector]
    public bool _timerOn = false;

    [HideInInspector]
    public bool _enableSpawnCourotine = false;

    [HideInInspector]
    public float _timer;

    private void Awake()
    {
        _timer = _timeAmountSO._timeAmount;
        ShowTimer(_timer);
        _spawnedObjects = new List<GameObject>();
    }

    public void StartSpawnCourotine()
    {
        _enableSpawnCourotine = true;
        _timerOn = true;

        StartCoroutine(SpawnCourotine());
    }

    public void SetTimerOn(bool value)
    {
        _timerOn = value;
    }

    public void StopSpawnCourotine()
    {
        _enableSpawnCourotine = false;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;

                ShowTimer(_timer);
            }
            else
            {
                _timerOn = false;
                ShowTimer(0f);

                foreach (GameObject spawnedObject in _spawnedObjects)
                {
                    if (spawnedObject != null)
                    {
                        ShipLife shiplife = spawnedObject.GetComponent<ShipLife>();
                        if(shiplife != null)
                        {
                            shiplife.TakeHit(1000);
                        }
                    }
                }

                StopSpawnCourotine();

                _endedGameSucessful?.Invoke();
            }
        }
    }

    private void ShowTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator SpawnCourotine()
    {
        do
        {
            Transform randomPos = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            GameObject enemySpawn = GameObject.Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)]);

            enemySpawn.transform.position = randomPos.position;
            _spawnedObjects.Add(enemySpawn);

            if (enemySpawn.TryGetComponent<MoveNearShip>(out MoveNearShip moveNearShip))
            {
                moveNearShip._playerShipLife = _playerTarget;
            }

            if (enemySpawn.TryGetComponent<ShipLife>(out ShipLife shipLife))
            {
                shipLife._spawner = this;
            }

            yield return new WaitForSeconds(_timeAmountSO._timeAmountSpawnEmenies);
        } while (_enableSpawnCourotine);
    }
}
