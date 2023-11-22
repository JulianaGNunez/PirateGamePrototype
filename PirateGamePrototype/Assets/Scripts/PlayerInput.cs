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
    public UnityEvent _requestShootCannon;
    public UnityEvent _requestShootCannonSpecial;

    public ShipLife _playerShip;

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
        _inputActions.Player.ShootCannon.performed += OnInputCannon;
        _inputActions.Player.ShootCannonSpecial.performed += OnInputCannonSpecial;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.MovementForward.performed -= OnInputForward;
        _inputActions.Player.MovementForward.canceled -= OnInputForwardReleased;
        _inputActions.Player.MovementRotation.performed -= OnInputRotation;
        _inputActions.Player.MovementRotation.canceled -= OnInputRotationReleased;
        _inputActions.Player.ShootCannon.performed -= OnInputCannon;
        _inputActions.Player.ShootCannonSpecial.performed -= OnInputCannonSpecial;
    }

    private void OnInputForward(InputAction.CallbackContext value)
    {
        if (_playerShip._shipLife <= 0)
        {
            return;
        }
        _forwardEvent?.Invoke(true);
    }

    private void OnInputForwardReleased(InputAction.CallbackContext value)
    {

        _forwardEvent?.Invoke(false);
    }

    private void OnInputRotation(InputAction.CallbackContext value)
    {
        if (_playerShip._shipLife <= 0)
        {
            return;
        }
        _rotateEvent?.Invoke(value.ReadValue<float>());
    }
    private void OnInputRotationReleased(InputAction.CallbackContext value)
    {
        _rotateEvent?.Invoke(0);
    }

    
    private void OnInputCannon(InputAction.CallbackContext value)
    {
        if (_playerShip._shipLife <= 0)
        {
            return;
        }
        _requestShootCannon?.Invoke();
    }

    private void OnInputCannonSpecial(InputAction.CallbackContext value)
    {
        if (_playerShip._shipLife <= 0)
        {
            return;
        }
        _requestShootCannonSpecial?.Invoke();
    }
}
