using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    GameObject _cannonBallPrefab;

    public bool _shootOnPlayer = false;


    public void ShootMethod(float rotation)
    {
        GameObject instance = GameObject.Instantiate(_cannonBallPrefab);
        instance.transform.SetParent(null);
        instance.transform.eulerAngles += new Vector3(0,0,rotation);
    }
}
