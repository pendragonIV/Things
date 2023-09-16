using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfIdleState : SkullWolfState
{
    public SkullWolfIdleState(SkullWolf skullWolf, SkullWolfStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void DoCheckUpdate()
    {
        base.DoCheckUpdate();
    }

    public override void Enter()
    {
        base.Enter();
        skullWolf.SetVelocityX(0f);
        skullWolf.SetVelocityY(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (skullWolf.checkPlayerInRange())
        {
            skullWolfStateMachine.ChangeState(skullWolf.attackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}