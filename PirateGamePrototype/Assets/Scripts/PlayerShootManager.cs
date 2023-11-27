using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerShootManager : MonoBehaviour
{
    public Shoot _shoot;
    public float _specialShootAngle = 30;

    public float _cooldownSingleShot;
    public float _cooldownSpecialShot;

    public Image _visualSpecialCannonTimer;

    private bool _cooldownSingleShotTrigger = true;
    private bool _cooldownSpecialShotTrigger = true;

    private bool _allowShoot = false;

    public void ShootCannonBall()
    {
        if (!_allowShoot)
        {
            return;
        }

        if (_cooldownSingleShotTrigger)
        {
            StartCoroutine(ShootCannonCourotine());
        }
    }

    public void ShootSpecialConnonBall()
    {
        if (!_allowShoot)
        {
            return;
        }

        if (_cooldownSingleShotTrigger && _cooldownSpecialShotTrigger)
        {
            StartCoroutine(ShootCannonSpecialCourotine());
            Sequence sequence = DOTween.Sequence();
            _visualSpecialCannonTimer.fillAmount = 0;
            sequence.Append(_visualSpecialCannonTimer.DOFillAmount(1, _cooldownSpecialShot).SetEase(Ease.Linear));
        }
    }

    private IEnumerator ShootCannonCourotine()
    {
        _cooldownSingleShotTrigger = false;
        _shoot?.ShootMethod(0);
        yield return new WaitForSeconds(_cooldownSingleShot);
        _cooldownSingleShotTrigger = true;
    }

    private IEnumerator ShootCannonSpecialCourotine()
    {
        _cooldownSingleShotTrigger = false;
        _cooldownSpecialShotTrigger = false;
        _shoot?.ShootMethod(-_specialShootAngle);
        _shoot?.ShootMethod(0);
        _shoot?.ShootMethod(_specialShootAngle);
        yield return new WaitForSeconds(_cooldownSingleShot);
        _cooldownSingleShotTrigger = true;
        yield return new WaitForSeconds(_cooldownSpecialShot - _cooldownSingleShot);
        _cooldownSpecialShotTrigger = true;
    }

    public void AllowShoot(bool value)
    {
        _allowShoot = value;
    }
}
