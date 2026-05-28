using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField] private float wrappingMargin = 1f;

    private float _horizontalLimit;
    private float _verticalLimit;

    private void Start()
    {
        _horizontalLimit = Camera.main.orthographicSize * Camera.main.aspect + wrappingMargin;
        _verticalLimit = Camera.main.orthographicSize + wrappingMargin;
    }

    private void Update()
    {
        Vector3 position = transform.position;

        //Horizontal wrapping
        if (position.x > _horizontalLimit)
            position.x = -_horizontalLimit;
        else if (position.x < -_horizontalLimit)
            position.x = _horizontalLimit;

        //Vertical wrapping
        if (position.y > _verticalLimit)
            position.y = -_verticalLimit;
        else if (position.y < -_verticalLimit)
            position.y = _verticalLimit;

        transform.position = position;
    }
}
