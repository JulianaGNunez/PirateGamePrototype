using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public Rigidbody2D _rigidbody;

    public int _force = 30;
    public int _maxForce = 100;

    public float _rotationSpeed = 10;

    private bool _enableMovementForward = false;

    private float _rotationDirection;

    private void FixedUpdate()
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

    public void SetEnableMovementForward(bool value)
    {
        _enableMovementForward = value;
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
        //_rigidbody.angularVelocity = Mathf.Clamp(_rigidbody.angularVelocity + (-_rotationDirection) * _rotationSpeed * Time.fixedDeltaTime, -40, 40);
        _rigidbody.MoveRotation(_rigidbody.rotation + (-_rotationDirection) * _rotationSpeed * Time.fixedDeltaTime);
    }
}
