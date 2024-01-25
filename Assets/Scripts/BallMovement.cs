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
    private LineRenderer _myLineRenderer;

    private bool _isDraging;

    private void Awake()
    {
        _myRigidbody2D = gameObject.GetComponentInChildren<Rigidbody2D>();
        _myLineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
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

        if (Input.GetMouseButton(RIGHT_MOUSE_BUTTON) && _isDraging)
        {
            DragChange(mousePosition);
        }

        if (Input.GetMouseButtonUp(RIGHT_MOUSE_BUTTON) && _isDraging)
        {
            DragRelease(mousePosition);
        }
    }

    private void DragStart()
    {
        _isDraging = true;
        _myLineRenderer.positionCount = 2;
    }

    private void DragChange(Vector2 position)
    {
        Vector2 direction = GetDirection(position);

        _myLineRenderer.SetPosition(0, transform.position);
        _myLineRenderer.SetPosition(1, (Vector2)transform.position +
            Vector2.ClampMagnitude((direction * _power) / 2, _maxPower / 2));
    }

    private void DragRelease(Vector2 position)
    {
        float distance = Vector2.Distance(transform.position, position);
        _isDraging = false;
        _myLineRenderer.positionCount = 0;

        if (distance < _minSpeedToRelease)
        {
            return;
        }

        Vector2 direction = GetDirection(position);

        _myRigidbody2D.velocity = Vector2.ClampMagnitude(direction * _power, _maxPower);
    }

    private Vector2 GetDirection(Vector2 position) => (Vector2)transform.position - position;

    private bool IsReady() => _myRigidbody2D.velocity.magnitude <= _minSpeedToMove;
}
