using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : PlayerState
{
    public DieState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.AudioPlayer.PlayPlayerDieClip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.PlayerMovement.Stop();
        player.PlayerHealth.Die(player.Animator);
    }
}
