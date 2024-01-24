using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private const int RIGHT_MOUSE_BUTTON = 0;

    [Header("Attributes")]
    [SerializeField] private float _minSpeedToMove = 0.2f;
    [SerializeField] private float _minSpeedToRelease = 1.0f;
    [SerializeField] private float _minDistanceToDrag = 0.5f;
    [SerializeField] private float _maxPower = 10.0f;
    [SerializeField] private float _power = 2.0f;
    [SerializeField] private float _maxGoalSpeed = 2.0f;

    private Rigidbody2D _myRigidbody2D;

    private bool _isDraging;

    private void Awake()
    {
        _myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsReady())
        {
            HandlePlayerInput();
        }
    }

    private void HandlePlayerInput()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, mousePosition);

        if (Input.GetMouseButtonDown(RIGHT_MOUSE_BUTTON) && distance <= _minDistanceToDrag)
        {
            DragStart();
        }

        if (Input.GetMouseButtonUp(RIGHT_MOUSE_BUTTON) && _isDraging)
        {
            DragRelease(mousePosition);
        }
    }

    private void DragStart()
    {
        _isDraging = true;
    }

    private void DragRelease(Vector2 position)
    {
        float distance = Vector2.Distance(transform.position, position);
        _isDraging = false;

        if (distance < _minSpeedToRelease)
        {
            return;
        }

        Vector2 direction = (Vector2)transform.position - position;

        _myRigidbody2D.velocity = Vector2.ClampMagnitude(direction * _power, _maxPower);
    }

    private bool IsReady() => _myRigidbody2D.velocity.magnitude <= _minSpeedToMove;
}
