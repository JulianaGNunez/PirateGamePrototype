using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeCanvasRenderer : MonoBehaviour
{
    public FadeImageUI _fadeImageUI;

    public CanvasGroup _canvasRenderer;

    public TMP_Text _pointsText;


    public Spawner _spawner;
    
    Sequence sequence;

    public void AppearCanvasRenderer()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();

        _pointsText.text = _pointsText.text.Replace("{x}", _spawner._playerPoints.ToString());
        sequence.Append(_canvasRenderer.DOFade(1f, 2f));
        sequence.OnComplete(() => { _canvasRenderer.interactable = true; });
    }

    public void ClickedButton(string sceneName)
    {
        _fadeImageUI?.FadeImage(1f, 1.5f, () => { LoadScene(sceneName); });
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
