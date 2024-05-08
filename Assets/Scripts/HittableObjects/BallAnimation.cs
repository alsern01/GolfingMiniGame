using UnityEngine;

public class BallAnimation : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.playerAnim = false;
        Destroy(animator.gameObject);

        if (GameManager.Instance.RoundFinished())
        {
            GameManager.Instance.EndRound();
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.playerAnim = true;
        InputManager.Instance.ResetMovement();
    }
}
