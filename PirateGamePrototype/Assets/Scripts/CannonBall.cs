using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int _damage = 1;
    public float _speed = 10f;

    public SpriteRenderer _ball;

    public Collider2D _ballCollider;

    [HideInInspector]
    public bool _ignorePlayerShip = false;

    private bool _enabledMovement = true;

    private void FixedUpdate()
    {
        if (_enabledMovement)
        {
            transform.position += transform.up * _speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DestroyCannonBall>(out DestroyCannonBall destroyCannonBall))
        {
            _enabledMovement = false;
            _ballCollider.enabled = false;
            _ball.enabled = false;
            Destroy(gameObject, 3f);
            return;
        }

        if (collision.TryGetComponent<ShipLife>(out ShipLife shipLife))
        {
            if (shipLife != null)
            {
                if (_ignorePlayerShip)
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
