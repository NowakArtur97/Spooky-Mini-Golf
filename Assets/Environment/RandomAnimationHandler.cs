using UnityEngine;

public class RandomAnimationHandler : MonoBehaviour
{
    [SerializeField] private int _numberOfAnimations = 3;

    private const string ANIMATION_TRIGGER = "animate";

    private Animator _myAnimator;

    private void Awake() => _myAnimator = GetComponent<Animator>();

    private void Start() => _myAnimator.SetTrigger(ANIMATION_TRIGGER + Random.Range(1, _numberOfAnimations + 1));
}
