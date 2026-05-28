using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 180f; // Degrees per second

    [Header("Thrust")]
    [SerializeField] private float forwardThrust = 8f;
    [SerializeField] private float reverseThrust = 4f;

    private Rigidbody2D _rigidBody;
    private PlayerInputActions _inputActions;
    private float _rotateInput;
    private float _thrustInput;

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

        _inputActions.Player.Thrust.performed += OnThrust;
        _inputActions.Player.Thrust.canceled += OnThrust;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();

        _inputActions.Player.Rotate.performed -= OnRotate;
        _inputActions.Player.Rotate.canceled -= OnRotate;

        _inputActions.Player.Thrust.performed -= OnThrust;
        _inputActions.Player.Thrust.canceled -= OnThrust;
    }

    private void OnThrust(InputAction.CallbackContext context)
    {
        _thrustInput = context.ReadValue<float>();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        _rotateInput = context.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        RotateShip();
        ApplyThrust();
    }

    private void ApplyThrust()
    {
        float thrust = _thrustInput > 0 ? forwardThrust : reverseThrust;
        _rigidBody.AddForce(transform.up * _thrustInput * thrust, ForceMode2D.Force);
    }

    private void RotateShip()
    {
        _rigidBody.MoveRotation(_rigidBody.rotation - _rotateInput * rotationSpeed * Time.fixedDeltaTime);
    }
}
