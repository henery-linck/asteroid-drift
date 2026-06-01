using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float minimumAngularSpeed = 10f;
    [SerializeField] private float maximumAngularSpeed = 90f;
    [SerializeField] private float minimumLinearSpeed = 2f;
    [SerializeField] private float maximumLinearSpeed = 10f;

    [Header("Direction Settings")]
    [SerializeField] private float minMargin = -10f;
    [SerializeField] private float maxMargin = 10f;

    [Header("General Settings")]
    [SerializeField] private float asteroidDuration = 15f;

    private float _linearSpeed;
    private float _angularSpeed;
    private Rigidbody2D _rigidBody;
    private GameObject _player;
    private float _timer;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        // Randomize initial speed and rotation of the asteroid
        _linearSpeed = Random.Range(minimumLinearSpeed, maximumLinearSpeed);
        _angularSpeed = Random.Range(minimumAngularSpeed, maximumAngularSpeed);

        //Randomize the direction of the asteroid within a margin of the player's ship direction
        float angle = Random.Range(_player.transform.eulerAngles.x + minMargin, _player.transform.eulerAngles.y + maxMargin) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Apply the calculated velocity and rotation to the asteroid's Rigidbody2D
        _rigidBody.linearVelocity = direction * _linearSpeed;
        _rigidBody.angularVelocity = _angularSpeed;
    }

    private void Update()
    {
        // Destroy the asteroid after a certain duration to prevent memory leaks
        _timer += Time.deltaTime;
        if (_timer >= asteroidDuration && IsOutsideCameraBounds())
            Destroy(gameObject);
    }

    private bool IsOutsideCameraBounds()
    {
        // Convert the object's position to Viewport coordinates
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // Check if the central pivot point is outside the screen limits
        return (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1);
    }
}
