using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlAnimation : MonoBehaviour
{
    public PlayerInput myPlayerInput;
    InputAction myAttackAction;
    InputAction myHitAction;
    InputAction mySwapAction;

    Animator myAnimator;
    AnimatorOverrideController myAnimatorOverrideController;
    public bool myHitWindow;

    public AnimationClip myAttack1;
    public AnimationClip myAttack2;

    private int myOverrideNum = 0;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimatorOverrideController = new AnimatorOverrideController(myAnimator.runtimeAnimatorController);

        myAttackAction = myPlayerInput.actions.FindAction("Attack");
        myHitAction = myPlayerInput.actions.FindAction("Hit");
        mySwapAction = myPlayerInput.actions.FindAction("Swap");
    }

    // Update is called once per frame
    void Update()
    {
        if(myAttackAction.WasPressedThisFrame())
        {
            myAnimator.SetTrigger("Attack");
        }

        if(myHitWindow && myHitAction.WasPressedThisFrame())
        {
            Debug.Log("HIT!");
            myHitWindow = false;
        }

        if(mySwapAction.WasPressedThisFrame())
        {
            Debug.Log("Swapping animation!");

            myAnimatorOverrideController = new AnimatorOverrideController(myAnimator.runtimeAnimatorController);
            
            if(myOverrideNum != 0)
            {
                myAnimatorOverrideController["attack"] = myAttack1;
                myOverrideNum = 0;
            }
            else
            {
                myAnimatorOverrideController["attack"] = myAttack2;
                myOverrideNum = 1;
            }

            myAnimator.runtimeAnimatorController = myAnimatorOverrideController;
        }
    }
}
