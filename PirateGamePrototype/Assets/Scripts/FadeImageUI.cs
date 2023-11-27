using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.Events;

public class FadeImageUI : MonoBehaviour
{
    public Image _fadeImage;

    public UnityEvent _startFadeEvent;

    Sequence _sequence;

    IEnumerator Start()
    {
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, 1f);
        yield return new WaitForSeconds(2f);
        FadeImage(0f, 1.5f, StartFadeEvent);
    }

    private void StartFadeEvent()
    {
        _startFadeEvent?.Invoke();
    }

    public void FadeImage(float amount, float time, Action finishAction)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(_fadeImage.DOFade(amount, time));
        _sequence.OnComplete(() => { finishAction?.Invoke(); });
    }
}
