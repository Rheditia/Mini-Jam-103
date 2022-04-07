using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnarmedState : PlayerState
{
    protected Vector2 input;
    protected bool takeSwordInput;
    protected PlayerMovement playerMovement;
    PlayerWeapon playerWeapon;
    PlayerHealth playerHealth;
    RemnantSpawner remnantSpawner;

    public UnarmedState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerWeapon = player.PlayerWeapon;
        playerMovement = player.PlayerMovement;
        playerHealth = player.PlayerHealth;
        remnantSpawner = player.RemnantSpawner;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.MoveInput;
        takeSwordInput = player.InputHandler.InterractInput;

        player.PlayerMovement.Jump();

        if (playerHealth.Health <= 0)
        {
            remnantSpawner.AddRemnant(0, player.transform.position);
            stateMachine.ChangeState(player.DieState);
        }

        if (takeSwordInput && playerWeapon.CheckWeaponHolderInRange() && playerWeapon.CheckWeaponHolderReadyStatus() && playerMovement.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.TakeSwordState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
