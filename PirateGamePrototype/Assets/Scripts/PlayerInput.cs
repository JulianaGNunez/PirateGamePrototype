using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputActions _inputActions;

    public UnityEvent<bool> _forwardEvent;

    public UnityEvent<float> _rotateEvent;

    private void Awake()
    {
        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.MovementForward.performed += OnInputForward;
        _inputActions.Player.MovementForward.canceled+= OnInputForwardReleased;
        _inputActions.Player.MovementRotation.performed += OnInputRotation;
        _inputActions.Player.MovementRotation.canceled += OnInputRotationReleased;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.MovementForward.performed -= OnInputForward;
        _inputActions.Player.MovementForward.canceled -= OnInputForwardReleased;
        _inputActions.Player.MovementRotation.performed -= OnInputRotation;
        _inputActions.Player.MovementRotation.canceled -= OnInputRotationReleased;
    }

    private void OnInputForward(InputAction.CallbackContext value)
    {
        _forwardEvent?.Invoke(true);
    }

    private void OnInputForwardReleased(InputAction.CallbackContext value)
    {
        _forwardEvent?.Invoke(false);
    }

    private void OnInputRotation(InputAction.CallbackContext value)
    {
        _rotateEvent?.Invoke(value.ReadValue<float>());
    }
    private void OnInputRotationReleased(InputAction.CallbackContext value)
    {
        _rotateEvent?.Invoke(0);
    }
}
