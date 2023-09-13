using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    protected bool isAnimationFinished;
    protected bool isAttackFinished;

    public PlayerAttackState(Player player, StateMachine stateMachine, PlayerProps playerProps, string animationName) : base(player, stateMachine, playerProps, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void DoCheckUpdate()
    {
        base.DoCheckUpdate();

        isAnimationFinished = !player.isAnimationPlaying(animationName);
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0);
        player.SetVelocityY(0);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished && isAttackFinished)
        {
            if(player.stateMachine.CurrentState == player.sideAttackState)
            {
                stateMachine.ChangeState(player.sideIdleState);
            }
            else if(player.stateMachine.CurrentState == player.backAttackState)
            {
                stateMachine.ChangeState(player.backIdleState);
            }
            else if(player.stateMachine.CurrentState == player.frontAttackState)
            {
                stateMachine.ChangeState(player.frontIdleState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
