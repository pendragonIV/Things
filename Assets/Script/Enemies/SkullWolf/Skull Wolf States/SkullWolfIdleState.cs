using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfIdleState : EnemyState
{
    public SkullWolfIdleState(SkullWolf skullWolf, EnemyStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
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
        enemy.SetVelocityX(0f);
        enemy.SetVelocityY(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.checkPlayerInRange())
        {
            enemyStateMachine.ChangeState(enemy.SideAttackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}