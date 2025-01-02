using UnityEngine;
using UnityEngine.InputSystem;

public class AttackRunner : MonoBehaviour
{
    public AttackSequence _attackSequence;
    public bool _hitWindow;

    Animator _animator;
    PlayerInput _playerInput;
    InputAction _attackAction;

    int _attackPhaseIndex = 0;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        _playerInput.SwitchCurrentActionMap("Player");
        _playerInput.currentActionMap.Enable();
        _attackAction = _playerInput.actions.FindAction("Attack");
    }

    void Start()
    {
        SetAnimationClipsFromAttackSequence(_attackSequence);
    }

    void Update()
    {
        if(_attackAction.IsPressed())
        {
            _animator.SetTrigger("TriggerAttackSequence");
            Debug.Log("Attack Sequence Triggered");
        }
    }

    void SetAnimationClipsFromAttackSequence(AttackSequence anAttackSequence)
    {
        AnimatorOverrideController _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);

        SetAttackPhaseClipOnOverride(_animatorOverrideController, "_SetupDefault", anAttackSequence._SetupPhase);
        SetAttackPhaseClipOnOverride(_animatorOverrideController, "_AttackDefault", anAttackSequence._AttackPhases[0]);
        SetAttackPhaseClipOnOverride(_animatorOverrideController, "_OnHitDefault", anAttackSequence._OnHitPhase);
        SetAttackPhaseClipOnOverride(_animatorOverrideController, "_OnMissDefault", anAttackSequence._OnMissPhase);
        SetAttackPhaseClipOnOverride(_animatorOverrideController, "_ResetDefault", anAttackSequence._ResetPhase);

        _animator.runtimeAnimatorController = _animatorOverrideController;
    }

    public void IncrementMultiAttack()
    {
        _attackPhaseIndex++;
        if(_attackPhaseIndex >= _attackSequence._AttackPhases.Length)
        {
            if(_attackSequence._repeatLastAttackPhase)
            {
                _attackPhaseIndex--;
                return;
            }
            else
            {
                _attackPhaseIndex = 0;
            }
        }

        AnimatorOverrideController _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        SetAttackPhaseClipOnOverride(_animatorOverrideController, "_AttackDefault", _attackSequence._AttackPhases[_attackPhaseIndex]);
        _animator.runtimeAnimatorController = _animatorOverrideController;
    }

    public int GetAttackPhaseIndex()
    {
        return _attackPhaseIndex;
    }

    void SetAttackPhaseClipOnOverride(AnimatorOverrideController anOverride, string defaultClipName, AttackPhase anAttackPhase)
    {
        if(anAttackPhase != null || anAttackPhase._AnimationClip != null)
        {
            anOverride[defaultClipName] = anAttackPhase._AnimationClip;
            Debug.Log(defaultClipName + " replaced by " + anAttackPhase._AnimationClip.name);
        }
    }

    public bool IsMultiAttack()
    {
        return _attackSequence._isMultiHit;
    }

    public void NextTarget()
    {
        SendMessage("IncrementTarget");
    }

    public AttackPhase GetAttackPhase(AttackPhaseType anAttackPhaseType, int attackIndex = 0)
    {
        switch(anAttackPhaseType)
        {
            case AttackPhaseType.SETUP:
                return _attackSequence._SetupPhase;
            case AttackPhaseType.ATTACK:
                return _attackSequence._AttackPhases[attackIndex];
            case AttackPhaseType.ONHIT:
                return _attackSequence._OnHitPhase;
            case AttackPhaseType.ONMISS:
                return _attackSequence._OnMissPhase;
            case AttackPhaseType.RESET:
                return _attackSequence._ResetPhase;
            default:
                Debug.LogError("Attack Phase not found. Returning Setup Phase.");
                return _attackSequence._SetupPhase;
        }
    }
}
