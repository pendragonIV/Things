using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBotDiagonalWalkState : PlayerBaseState
{
    public PlayerBotDiagonalWalkState(Player player, StateMachine stateMachine, PlayerProps playerProps, string animationName) : base(player, stateMachine, playerProps, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if (InputHorizontal == 0 && InputVertical == 0)
        {
            stateMachine.ChangeState(player.sideIdleState);
        }
        else if (InputHorizontal == 0 && InputVertical < 0)
        {
            stateMachine.ChangeState(player.frontWalkState);
        }
        else if (InputHorizontal != 0 && InputVertical == 0)
        {
            stateMachine.ChangeState(player.sideWalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        InputHorizontal *= playerProps.diagonalWalkSpeed;
        InputVertical *= playerProps.diagonalWalkSpeed;

        player.HorizontalFlip(InputHorizontal);

        player.SetVelocityX(playerProps.walkSpeed * InputHorizontal);
        player.SetVelocityY(playerProps.walkSpeed * InputVertical);

    }
}
