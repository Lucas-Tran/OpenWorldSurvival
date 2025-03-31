using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public event EventHandler OnInteract;
    public event EventHandler OnToggleInventory;

    public static GameInput Instance { get; private set; }

    private PlayerInputActions inputActions;

    private void Awake() {
        Instance = this;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.ToggleInventory.performed += ToggleInventory_performed;
    }

    private void ToggleInventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnToggleInventory?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMoveVectorNormalized() {
        Vector2 inputVector = inputActions.Player.Walk.ReadValue<Vector2>();

        return inputVector;
    }

    public Vector2 GetLook() {
        Vector2 inputAxis = inputActions.Player.Look.ReadValue<Vector2>();

        return inputAxis;
    }


}