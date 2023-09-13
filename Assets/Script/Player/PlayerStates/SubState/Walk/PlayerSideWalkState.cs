using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideWalkState : PlayerBaseState
{
    public PlayerSideWalkState(Player player, StateMachine stateMachine, PlayerProps playerProps, string animationName) : base(player, stateMachine, playerProps, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (InputHorizontal == 0)
        {
            stateMachine.ChangeState(player.sideIdleState);
        }
        else if (InputVertical > 0)
        {
            stateMachine.ChangeState(player.topDiagonalWalkState);
        }
        else if (InputVertical < 0)
        {
            stateMachine.ChangeState(player.bottomDiagonalWalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (InputHorizontal != 0) {
            player.HorizontalFlip(InputHorizontal);

            player.SetVelocityX(InputHorizontal * playerProps.walkSpeed);
        }
    }
}
