using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int _damage = 1;
    public float _speed = 10f;

    [HideInInspector]
    public bool _ignorePlayerShip = false;

    private void FixedUpdate()
    {
        transform.position += transform.up * _speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HitShip");
        if (collision.TryGetComponent<ShipLife>(out ShipLife shipLife))
        {
            if(shipLife != null)
            {
                if (shipLife._playerShip && _ignorePlayerShip)
                {
                    _ignorePlayerShip = false;
                    return;
                }

                shipLife.TakeHit(_damage);
                Destroy(gameObject);
            }
        }
    }
}
