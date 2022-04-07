using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedRunState : ArmedState
{
    public ArmedRunState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
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
        playerMovement.Jump();

        if (attackInput && playerMovement.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.ArmedAttackState);
        }

        if (Mathf.Abs(input.x) < 0.01f)
        {
            stateMachine.ChangeState(player.ArmedIdleState);
        }
    }
}
