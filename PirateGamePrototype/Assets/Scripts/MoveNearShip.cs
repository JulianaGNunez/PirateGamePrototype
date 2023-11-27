using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;
using System.Drawing;

public class MoveNearShip : MonoBehaviour
{
    public Shoot _shoot;

    public Rigidbody2D _rigidbody;

    public Collider2D _selfCollider;

    public ShipLife _selfShipLife;

    public ShipLife _playerShipLife;

    public bool _enableShoot = true;

    public bool _explodeOnCollision = false;

    public int _damageOnCollision = 0;

    private bool _reloadedCannon = true;

    public float _distanceToStop = 10;

    public float _forceForward = 100;
    public float _maxForceForward = 1000;

    public float _speedRotation = 30f;
    public float _reloadCannonTime = 1.5f;
    public float _angleToShoot = 5f;

    public void FixedUpdate()
    {
        if (_selfShipLife._shipLife <= 0 || _playerShipLife._shipLife <= 0)
        {
            return;
        }

        RotateShip();
        if (Mathf.Abs(Vector2.Distance(_playerShipLife.transform.position, transform.position)) > _distanceToStop)
        {
            MoveShipFoward();
        }
        else
        {
            if (_enableShoot)
            {
                DecideToShoot();
            }
        }

    }

    private IEnumerator ShootCannon()
    {
        _reloadedCannon = false;
        _shoot.ShootMethod(0);
        yield return new WaitForSeconds(_reloadCannonTime);
        _reloadedCannon = true;
    }

    public void MoveShipFoward()
    {
        _rigidbody.AddForce(_rigidbody.transform.up * _forceForward * Time.fixedDeltaTime);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxForceForward);
    }

    private void RotateShip()
    {
        Vector3 vectorToTarget = _playerShipLife.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _speedRotation);
    }

    private void DecideToShoot()
    {
        if (Vector2.Angle(transform.eulerAngles, _playerShipLife.transform.eulerAngles) < _angleToShoot)
        {
            if (_reloadedCannon)
            {
                StartCoroutine(ShootCannon());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_explodeOnCollision)
        {
            if (collision.gameObject.TryGetComponent<ShipLife>(out ShipLife shipLife))
            {
                if (shipLife != null && shipLife._playerShip)
                {
                    _selfCollider.enabled = false;
                    _selfShipLife?.TakeHit(1000);
                    shipLife.TakeHit(_damageOnCollision);
                }
            }
        }
    }
}
