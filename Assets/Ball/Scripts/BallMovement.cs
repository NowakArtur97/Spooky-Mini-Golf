using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private const int RIGHT_MOUSE_BUTTON = 0;

    [Header("Movement")]
    [SerializeField] private float _minSpeedToMove = 0.2f;
    [SerializeField] private float _minSpeedToRelease = 1.0f;
    [SerializeField] private float _minDistanceToDrag = 0.5f;
    [SerializeField] private float _maxPower = 10.0f;
    [SerializeField] private float _power = 2.0f;

    [Header("Shrinking")]
    [SerializeField] private float _minSize = 0.2f;
    [SerializeField] private float _shrinkingSpeed = 2.0f;

    private Rigidbody2D _myRigidbody2D;
    private LineRendererHandler _lineRendererHandler;

    private bool _isDraging;
    private bool _isInHole;

    private void Awake()
    {
        _myRigidbody2D = gameObject.GetComponentInChildren<Rigidbody2D>();
        _lineRendererHandler = gameObject.GetComponentInChildren<LineRendererHandler>();
    }

    private void Update()
    {
        if (IsReady())
        {
            HandlePlayerInput();
        }

        if (_isInHole)
        {
            ShrinkBall();
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
        _lineRendererHandler.StartRendering();
    }

    private void DragChange(Vector2 position)
    {
        Vector2 direction = GetDirection(position);

        _lineRendererHandler.Render(direction, transform.position, _power, _maxPower);
    }

    private void DragRelease(Vector2 position)
    {
        float distance = Vector2.Distance(transform.position, position);
        _isDraging = false;
        _lineRendererHandler.StopRendering();

        if (distance < _minSpeedToRelease)
        {
            return;
        }

        Vector2 direction = GetDirection(position);

        _myRigidbody2D.velocity = Vector2.ClampMagnitude(direction * _power, _maxPower);
    }

    public float GetVelocityMagnitude() => _myRigidbody2D.velocity.magnitude;

    public void StartShrinking() => _isInHole = true;

    private void ShrinkBall()
    {
        if (transform.localScale.y > _minSize)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * _shrinkingSpeed,
                transform.localScale.y - Time.deltaTime * _shrinkingSpeed, transform.localScale.z);
        }
        else
        {
            _myRigidbody2D.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private Vector2 GetDirection(Vector2 position) => (Vector2)transform.position - position;

    private bool IsReady() => _myRigidbody2D.velocity.magnitude <= _minSpeedToMove;
}
