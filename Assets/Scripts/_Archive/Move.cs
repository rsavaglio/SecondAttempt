using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public PlayerInput myPlayerInput;

    InputAction myMoveAction;
    InputAction myAttackAction;

    Animator myAnimator;
   
    private int myAttackCount = 0;

    void OnEnable()
    {
        myPlayerInput.SwitchCurrentActionMap("Player");
        myPlayerInput.currentActionMap.Enable();
        
        myMoveAction = myPlayerInput.actions.FindAction("Move");
        myAttackAction = myPlayerInput.actions.FindAction("Attack");
        myAttackAction.performed += AttackAction;
    }

    void OnDisable()
    {
        myPlayerInput.actions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = myMoveAction.ReadValue<Vector2>();
        transform.Translate(moveVector * 0.5f, 0);
    }

    private void AttackAction(InputAction.CallbackContext context)
    {
        Debug.Log("Attacked: " + myAttackCount.ToString());
        myAttackCount++;
    }
}