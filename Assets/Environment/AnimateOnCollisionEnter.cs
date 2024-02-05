using UnityEngine;

public class AnimateOnCollisionEnter : MonoBehaviour
{
    private const string ANIMATION_TRIGGER = "animate";

    private Animator _myAnimator;

    private void Awake() => _myAnimator = GetComponent<Animator>();

    private void OnCollisionEnter2D(Collision2D collision) => _myAnimator.SetTrigger(ANIMATION_TRIGGER);
}

