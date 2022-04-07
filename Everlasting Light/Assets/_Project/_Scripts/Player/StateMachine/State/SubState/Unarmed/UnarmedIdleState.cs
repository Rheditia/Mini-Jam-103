using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnarmedIdleState : UnarmedState
{
    public UnarmedIdleState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
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

        if(Mathf.Abs(input.x) > 0.01)
        {
            stateMachine.ChangeState(player.UnarmedRunState);
        }
    }
}
