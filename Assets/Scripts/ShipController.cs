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
    [SerializeField] private float maxSpeedForward = 16f;
    [SerializeField] private float maxSpeedReverse = 8f;
    [SerializeField] private SpriteRenderer thrustSpriteRenderer;
    [SerializeField] private Animator thrustAnimator;

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
        thrustSpriteRenderer.enabled = _thrustInput > 0;
        thrustAnimator.SetBool("IsAccelerating", _thrustInput > 0);
        
        float thrust = _thrustInput > 0 ? forwardThrust : reverseThrust;
        float maxSpeed = _thrustInput > 0 ? maxSpeedForward : maxSpeedReverse;

        _rigidBody.AddForce(transform.up * _thrustInput * thrust, ForceMode2D.Force);
        _rigidBody.linearVelocity = Vector2.ClampMagnitude(_rigidBody.linearVelocity, maxSpeed);
    }

    private void RotateShip()
    {
        _rigidBody.MoveRotation(_rigidBody.rotation - _rotateInput * rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
