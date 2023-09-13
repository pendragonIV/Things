using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackWalkState : PlayerBaseState
{
    public PlayerBackWalkState(Player player, StateMachine stateMachine, PlayerProps playerProps, string animationName) : base(player, stateMachine, playerProps, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
      
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(InputVertical == 0)
        {
            stateMachine.ChangeState(player.backIdleState);
        }
        else if(InputHorizontal != 0)
        {
            stateMachine.ChangeState(player.topDiagonalWalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (InputVertical > 0) {
            player.SetVelocityY(InputVertical * playerProps.walkSpeed);
        }
    }
}
