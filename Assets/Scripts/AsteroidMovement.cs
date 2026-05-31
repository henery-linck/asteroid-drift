using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private float minimumAngularSpeed = 10f;
    [SerializeField] private float maximumAngularSpeed = 90f;
    [SerializeField] private float minimumLinearSpeed = 2f;
    [SerializeField] private float maximumLinearSpeed = 10f;

    private float _linearSpeed;
    private float _angularSpeed;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        // Randomize initial speed and direction
        _linearSpeed = Random.Range(minimumLinearSpeed, maximumLinearSpeed);
        _angularSpeed = Random.Range(minimumAngularSpeed, maximumAngularSpeed);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        _rigidBody.linearVelocity = direction * _linearSpeed;
        _rigidBody.angularVelocity = _angularSpeed;
    }
}
