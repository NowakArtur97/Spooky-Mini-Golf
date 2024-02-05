using System.Collections;
using UnityEngine;

public class HandAnimationHandler : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float _hidenTime = 3.0f;
    [SerializeField] private float _idleTime = 3.0f;

    private Animator _myAnimator;

    private void Awake() => _myAnimator = GetComponent<Animator>();

    private void Start() => StartCoroutine(Wait(_hidenTime, "moveUp"));

    public void TriggerHiddenState() => StartCoroutine(Wait(_hidenTime, "moveUp"));

    public void TriggerIdleState() => StartCoroutine(Wait(_idleTime, "moveDown"));

    private IEnumerator Wait(float waitTime, string triggerName)
    {
        yield return new WaitForSeconds(waitTime);

        _myAnimator.SetTrigger(triggerName);
    }
}
