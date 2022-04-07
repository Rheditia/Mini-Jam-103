using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedIdleState : ArmedState
{
    public ArmedIdleState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerMovement.Move();
        player.PlayerMovement.Jump();

        if (attackInput && playerMovement.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.ArmedAttackState);
        }

        if (Mathf.Abs(input.x) > 0.01f)
        {
            stateMachine.ChangeState(player.ArmedRunState);
        }
    }
}
