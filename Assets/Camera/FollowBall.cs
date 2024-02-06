using UnityEngine;

public class FollowBall : MonoBehaviour
{
    private Transform _ballMovement;

    private void Awake() => _ballMovement = FindObjectOfType<BallMovement>().gameObject.transform;

    private void Update() => transform.position = new Vector3(_ballMovement.position.x, _ballMovement.position.y, transform.position.z);
}
