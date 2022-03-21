using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(bool isWalking)
    {
        _animator.SetBool("Walking", isWalking);
    }

    public void AttackAnimation(bool isAttacking)
    {
        _animator.SetBool("Attacking", isAttacking);
    }
}
