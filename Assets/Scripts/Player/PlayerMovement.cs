using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AnimationTrigger", false);
        //GameManager.Instance.playerAnim = false;
        // golpear
        if (GameManager.Instance.ballCreated)
        {
            GameManager.Instance.Invoke("HitAnimation", 1.2f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
