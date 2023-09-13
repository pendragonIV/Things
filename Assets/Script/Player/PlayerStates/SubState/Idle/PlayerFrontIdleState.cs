using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrontIdleState : PlayerBaseState
{
    public PlayerFrontIdleState(Player player, StateMachine stateMachine, PlayerProps playerProps, string animationName) : base(player, stateMachine, playerProps, animationName)
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

        if(InputHorizontal != 0)
        {
            stateMachine.ChangeState(player.sideWalkState);
        }
        else if(InputVertical < 0)
        {
            stateMachine.ChangeState(player.frontWalkState);
        }
        else if(InputVertical > 0)
        {
            stateMachine.ChangeState(player.backWalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(0);
        player.SetVelocityY(0);
    }
}
