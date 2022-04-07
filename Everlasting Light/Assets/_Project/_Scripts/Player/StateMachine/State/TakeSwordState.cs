using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSwordState : PlayerState
{
    PlayerWeapon playerWeapon;
    WeaponState swordState;
    Vector2 input;

    public TakeSwordState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerWeapon = player.PlayerWeapon;
        playerWeapon.TakeWeapon = true;
        playerWeapon.WeaponHolderInterraction();

        swordState = playerWeapon.SwordState;
        player.AudioPlayer.PlaySwordClip();

        if (swordState == WeaponState.Blue)
        {
            player.Animator.SetBool("blueSword", true);
        }
        else if (swordState == WeaponState.Red)
        {
            player.Animator.SetBool("redSword", true);
        }
        //playerWeapon.AnimationDelay(player.Animator);
    }

    public override void Exit()
    {
        base.Exit();
        playerWeapon.TakeWeapon = false;
        //playerWeapon.IsAnimationDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.MoveInput;
        player.PlayerMovement.Stop();
        if (playerWeapon.AnimationStatusCheck(player.Animator))
        {
            if (Mathf.Abs(input.x) > 0.01f) { stateMachine.ChangeState(player.ArmedRunState); }
            else { stateMachine.ChangeState(player.ArmedIdleState); }
        }
    }
}
