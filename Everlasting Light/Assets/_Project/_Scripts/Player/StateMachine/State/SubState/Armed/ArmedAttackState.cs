using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedAttackState : ArmedState
{
    public ArmedAttackState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.AudioPlayer.PlayPlayerAttackClip();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerMovement.Stop();
        if (playerWeapon.AnimationStatusCheck(player.Animator))
        {
            playerWeapon.Attack();
            if (Mathf.Abs(input.x) < 0.01f)
            {
                stateMachine.ChangeState(player.ArmedIdleState);
            }
            else if (Mathf.Abs(input.x) > 0.01f)
            {
                stateMachine.ChangeState(player.ArmedRunState);
            }
        }
    }
}
