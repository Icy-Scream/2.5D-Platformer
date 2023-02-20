using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private ActionMaps _input;
    public event EventHandler OnInteract;
    private void Awake()
    {
        _input = new ActionMaps();
    }

    private void OnEnable()
    {
        _input.Player.Enable();
        _input.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() 
    {
        Vector2 _direction = _input.Player.Move.ReadValue<Vector2>();
        _direction = _direction.normalized;
        return _direction;
    }

    public bool IsJumping() 
    {
        if (_input.Player.Jump.IsPressed())
            return true;
        else return false;
    }
}
