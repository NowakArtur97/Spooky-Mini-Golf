using UnityEngine;

public class GraveHole : MonoBehaviour
{
    private const string BALL_TAG = "Ball";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ball = collision.gameObject.transform.parent.gameObject;

        if (ball.tag == BALL_TAG)
        {
            ball.GetComponent<BallMovement>()?.StopMovement();
        }
    }
}
