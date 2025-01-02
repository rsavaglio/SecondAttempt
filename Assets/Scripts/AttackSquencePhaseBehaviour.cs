using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public enum AttackPhaseType
{
    SETUP,
    ATTACK,
    ONHIT,
    ONMISS,
    RESET
}

public class AttackSquencePhaseBehaviour : StateMachineBehaviour
{
    public AttackPhaseType _attackPhaseType;
    
    AttackRunner _attackRunner;
    InputAction _hitAction;

    AttackPositionAnimator _attackPositionAnimator;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attackRunner = animator.GetComponent<AttackRunner>();
        _hitAction = animator.GetComponent<PlayerInput>().actions.FindAction("Hit");
       
        _attackPositionAnimator = animator.GetComponent<AttackPositionAnimator>();
        _attackPositionAnimator.SetAttackPhase(_attackRunner.GetAttackPhase(_attackPhaseType, _attackRunner.GetAttackPhaseIndex()));

        Debug.Log("Attack Phase " + GetPhaseTypeName(_attackPhaseType) + " was started.");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attackPositionAnimator.DisableInterpolation();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_attackRunner._hitWindow && _hitAction.IsPressed())
        {
            if(_attackRunner.IsMultiAttack())
            {
                animator.SetTrigger("TriggerMulti");
                _attackRunner.IncrementMultiAttack();
                _attackRunner.NextTarget();
            }
            else
            {
                animator.SetTrigger("TriggerHit");
            }
        }
    }

    string GetPhaseTypeName(AttackPhaseType anAttackPhaseType)
    {
        switch(anAttackPhaseType)
        {
            case AttackPhaseType.SETUP:
                return "Setup";
            case AttackPhaseType.ATTACK:
                return "Attack";
            case AttackPhaseType.ONHIT:
                return "OnHit";
            case AttackPhaseType.ONMISS:
                return "OnMiss";
            case AttackPhaseType.RESET:
                return "Reset";
            default:
                return "GetPhaseTypeName - AttackPhaseType not found!";
        }
    }
}
