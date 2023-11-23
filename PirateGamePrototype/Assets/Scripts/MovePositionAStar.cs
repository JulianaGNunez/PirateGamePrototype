using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using static UnityEngine.GridBrushBase;
using System.Drawing;

public class MovePositionAStar : MonoBehaviour
{
    public AIPath _aiPath;

    public Shoot _shoot;

    public Rigidbody2D _rigidbody;

    public Transform _target;

    private bool _reloadedCannon = true;

    public float _speedRotation = 30f;
    public float _reloadCannonTime = 1.5f;
    public float _angleToShoot = 5f;

    public void FixedUpdate()
    {
        if (_aiPath.reachedDestination)
        {
            _aiPath.isStopped = true;
        }

        if (_aiPath.isStopped)
        {
            RotateShip();
        }
        else
        {
            _aiPath.destination = _target.position;
        }

    }

    private IEnumerator ShootCannon()
    {
        _reloadedCannon = false;
        _shoot.ShootMethod(0);
        yield return new WaitForSeconds(_reloadCannonTime);
        _reloadedCannon = true;
        _aiPath.isStopped = false;
        _aiPath.destination = _target.position;
    }

    private void RotateShip()
    {
        Vector3 direction = _target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, transform.right);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _speedRotation * Time.fixedDeltaTime);
        if (Vector2.Angle(transform.eulerAngles, _target.eulerAngles) < _angleToShoot)
        {
            if (_reloadedCannon)
            {
                StartCoroutine(ShootCannon());
            }
        }
    }
}
