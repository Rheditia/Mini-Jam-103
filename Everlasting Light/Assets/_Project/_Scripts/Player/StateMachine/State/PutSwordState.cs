using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutSwordState : PlayerState
{
    PlayerWeapon playerWeapon;
    WeaponState swordState;
    Vector2 input;

    public PutSwordState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerWeapon = player.PlayerWeapon;
        swordState = playerWeapon.SwordState;
        player.AudioPlayer.PlaySwordClip();
    }

    public override void Exit()
    {
        base.Exit();
        playerWeapon.PutWeapon = false;
        player.Animator.SetBool("blueSword", false);
        player.Animator.SetBool("redSword", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.MoveInput;
        player.PlayerMovement.Stop();
        if (playerWeapon.AnimationStatusCheck(player.Animator))
        {
            playerWeapon.PutWeapon = true;
            playerWeapon.WeaponHolderInterraction();
            if(Mathf.Abs(input.x) > 0.01f) { stateMachine.ChangeState(player.UnarmedRunState); }
            else { stateMachine.ChangeState(player.UnarmedIdleState); }
        }
    }
}
