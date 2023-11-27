using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShipLife : MonoBehaviour
{
    public int _shipLife;

    private int _maxShipLife;

    public bool _playerShip;

    private bool _invulnerable = false;

    public Action _shipDestroyed;

    public Animator _shipAnimator;

    public TextMeshPro _lifeText;

    public UnityEvent _playerDestroyed;

    public SpriteRenderer _shipBody;
    public SpriteRenderer _shipSail;

    public int _lifeConsiderAlmostSinking = 2;
    public Sprite _shipHit;
    public Sprite _shipAlmostSinking;
    public Sprite _sailsAlmostSinking;

    [HideInInspector]
    public Spawner _spawner;

    private void Awake()
    {
        _maxShipLife = _shipLife;

        _lifeText.text = _shipLife + "/" + _maxShipLife;
    }

    public void TakeHit(int hitDamage)
    {
        if(_invulnerable == true)
        {
            return;
        }

        _shipLife -= hitDamage;

        _shipLife = _shipLife < 0 ? 0 : _shipLife;

        _lifeText.text = _shipLife + "/" + _maxShipLife;

        if (_shipLife > 0)
        {
            _shipAnimator?.SetTrigger("HitDamage");
        }
        else
        {
            _shipAnimator?.SetTrigger("DestroyedShip");
        }


        if (_shipLife > _lifeConsiderAlmostSinking)
        {
            _shipBody.sprite = _shipHit;
        }
        else
        {
            _shipBody.sprite = _shipAlmostSinking;
            if(_sailsAlmostSinking != null)
            {
                _shipSail.sprite = _sailsAlmostSinking;
            }
        }

        if(_spawner != null && _spawner._timer > 0)
        {
            _spawner._playerPoints += 1;
        }

        if (_playerShip)
        {
            _invulnerable = true;
        }
    }

    public void SetInvulnerable(bool value)
    {
        _invulnerable = value;
    }

    public void EndedHitDamageAnimation()
    {
        _invulnerable = false;
    }

    public void EndedDestroyedShipAnimation()
    {
        _shipDestroyed?.Invoke();
        if (_playerShip)
        {
            gameObject.SetActive(false);
            _playerDestroyed?.Invoke();
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
