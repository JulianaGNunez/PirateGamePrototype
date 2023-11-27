using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingsInput : MonoBehaviour
{
    TMP_InputField _inputField;

    public int _defaultTime;

    public int _maxValue;
    public int _minValue;

    public UnityEvent _startEvent;

    public TimeAmountSO _timeAmountSO;

    private void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.contentType = TMP_InputField.ContentType.IntegerNumber;

        _inputField.text = _defaultTime.ToString();

        _startEvent?.Invoke();

        _inputField.onEndEdit.AddListener(ParseInput);
    }

    private void ParseInput(string value)
    {
        Int32.TryParse(_inputField.text, out int valueInt);
        ValidateInput(valueInt);
    }

    void ValidateInput(int value)
    {
        if (value > _maxValue)
        {
            _inputField.text = _maxValue.ToString();
        }
        else if (value <= _minValue)
        {
            _inputField.text = _minValue.ToString();
        }
    }

    public void CompletedInputWaveTime()
    {
        Int32.TryParse(_inputField.text, out int valueInt);
        _timeAmountSO._timeAmount = (float)valueInt;
    }

    public void CompletedInputEnemySpawnTime()
    {
        Int32.TryParse(_inputField.text, out int valueInt);
        _timeAmountSO._timeAmountSpawnEmenies = (float)valueInt;
    }
}
