using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLife : MonoBehaviour
{
    public int _shipLife;

    public bool _playerShip;

    private bool _invulnerable = false;

    public Action _shipDestroyed;

    public Animator _shipAnimator;

    public void TakeHit(int hitDamage)
    {
        if(_invulnerable == true)
        {
            return;
        }

        _shipLife -= hitDamage;

        if(_shipLife > 0)
        {
            _shipAnimator?.SetTrigger("HitDamage");
        }
        else
        {
            _shipAnimator?.SetTrigger("DestroyedShip");
        }

        if (_playerShip)
        {
            _invulnerable = true;
        }
    }

    public void EndedHitDamageAnimation()
    {
        _invulnerable = false;
    }

    public void EndedDestroyedShipAnimation()
    {
        _shipDestroyed?.Invoke();
        Destroy(gameObject);
    }

}
