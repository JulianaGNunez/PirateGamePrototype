using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public ShipLife _shipLife;

    public int _force = 30;
    public int _maxForce = 100;

    public float _rotationSpeed = 10;

    private bool _enableMovementForward = false;

    private float _rotationDirection;

    [HideInInspector]
    public bool _allowMovement = false;

    private void Start()
    {
        _shipLife._shipDestroyed += SetShipDestroyed;
    }

    private void FixedUpdate()
    {
        if (_allowMovement)
        {
            if (_enableMovementForward)
            {
                MoveShipFoward();
            }

            if (_rotationDirection != 0)
            {
                RotateShip();
            }
        }
    }

    public void AllowMovement(bool value)
    {
        _allowMovement = value;
    }

    public void SetEnableMovementForward(bool value)
    {
        _enableMovementForward = value;
    }

    public void SetShipDestroyed()
    {
        _allowMovement = false;
        _enableMovementForward = false;
        _rotationDirection = 0;
    }

    public void SetRotationDirection(float value)
    {
        _rotationDirection = value;
    }


    public void MoveShipFoward()
    {
        _rigidbody.AddForce(_rigidbody.transform.up * _force * Time.fixedDeltaTime);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxForce);
    }

    public void RotateShip()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation + (-_rotationDirection) * _rotationSpeed * Time.fixedDeltaTime);
    }
}
