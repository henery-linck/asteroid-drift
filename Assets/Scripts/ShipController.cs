using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 180f; // Degrees per second

    private Rigidbody2D _rigidBody;
    private PlayerInputActions _inputActions;
    private float _rotateInput;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();

        _inputActions.Player.Rotate.performed += OnRotate;
        _inputActions.Player.Rotate.canceled += OnRotate;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();

        _inputActions.Player.Rotate.performed -= OnRotate;
        _inputActions.Player.Rotate.canceled -= OnRotate;
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        _rotateInput = context.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        RotateShip();
    }

    private void RotateShip()
    {
        _rigidBody.MoveRotation(_rigidBody.rotation - _rotateInput * rotationSpeed * Time.fixedDeltaTime);
    }
}
