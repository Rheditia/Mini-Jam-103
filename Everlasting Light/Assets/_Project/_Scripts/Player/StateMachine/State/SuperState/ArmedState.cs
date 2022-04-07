using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedState : PlayerState
{
    protected Vector2 input;
    protected bool putSwordInput;
    protected bool attackInput;

    protected PlayerMovement playerMovement;
    protected PlayerWeapon playerWeapon;
    PlayerHealth playerHealth;
    RemnantSpawner remnantSpawner;

    WeaponState swordState;

    public ArmedState(Player player, PlayerStateMachine stateMachine, string animationBool) : base(player, stateMachine, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerWeapon = player.PlayerWeapon;
        playerMovement = player.PlayerMovement;
        playerHealth = player.PlayerHealth;
        remnantSpawner = player.RemnantSpawner;

        swordState = playerWeapon.SwordState;
        //Debug.Log(swordState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.MoveInput;
        putSwordInput = player.InputHandler.InterractInput;
        attackInput = player.InputHandler.AttackInput;

        if (playerHealth.Health <= 0)
        {
            remnantSpawner.AddRemnant(1, player.transform.position);
            stateMachine.ChangeState(player.DieState);
        }

        if ((swordState == WeaponState.Blue && playerWeapon.Charge <= 0))
        {
            remnantSpawner.AddRemnant(0, player.transform.position);
            stateMachine.ChangeState(player.DieState);
        }

        if (swordState == WeaponState.Red && playerWeapon.Charge > 5)
        {
            remnantSpawner.AddRemnant(1, player.transform.position);
            stateMachine.ChangeState(player.DieState);
        }

        if (putSwordInput && playerWeapon.CheckWeaponHolderInRange() && playerMovement.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.PutSwordState);
        }
    }
}
