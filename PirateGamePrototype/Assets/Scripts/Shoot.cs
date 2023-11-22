using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject _cannonBallPrefab;

    public bool _ignorePlayerShip = false;


    public void ShootMethod(float rotation)
    {
        GameObject instance = GameObject.Instantiate(_cannonBallPrefab);
        instance.transform.SetParent(null);
        instance.transform.position = transform.position;
        instance.transform.eulerAngles = new Vector3(0,0, transform.localEulerAngles.z + rotation);

        if (instance.TryGetComponent<CannonBall>(out CannonBall cannonBall))
        {
            cannonBall._ignorePlayerShip = _ignorePlayerShip;
        }
    }
}
