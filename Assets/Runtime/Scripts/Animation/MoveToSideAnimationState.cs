using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSideAnimationState : StateMachineBehaviour
{
    private ObstacleMove obstacleMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo runClip = clips[0];
            obstacleMove = animator.transform.parent.parent.parent.GetComponent<ObstacleMove>();
            float timeToCompleteAnimationState = obstacleMove.SideToSideMoveTime * 2;
            float speedMultiplier = runClip.clip.length / timeToCompleteAnimationState;
            animator.SetFloat(ObstacleAnimationConsts.SideToSideMultiplier, speedMultiplier);
        }
    }
}
