using UnityEngine;

public class HoleManager : MonoBehaviour
{
    private const string BALL_TAG = "Ball";

    [SerializeField] private float _maxGoalSpeed = 2.0f;

    private bool _inHole;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ball = collision.gameObject.transform.parent.gameObject;

        if (ball.tag == BALL_TAG)
        {
            CheckIfIsInHole(ball);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject ball = collision.gameObject;

        if (ball.tag == BALL_TAG)
        {
            CheckIfIsInHole(ball);
        }
    }

    private void CheckIfIsInHole(GameObject ball)
    {
        if (_inHole)
        {
            return;
        }

        BallMovement ballMovement = ball.GetComponent<BallMovement>();

        if (ballMovement?.GetVelocityMagnitude() <= _maxGoalSpeed)
        {
            _inHole = true;

            ballMovement.StopMovement();
        }
    }
}
