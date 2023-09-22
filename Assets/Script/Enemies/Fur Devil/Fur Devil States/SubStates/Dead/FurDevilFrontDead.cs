using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurDevilFrontDead : FurDevilBaseState
{
    public FurDevilFrontDead(Enemy enemy, EnemyStateMachine enemyStateMachine, EnemiesDataSO enemyData, string animationName) : base(enemy, enemyStateMachine, enemyData, animationName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!enemy.isAnimationPlaying(animationName))
        {
            enemy.Die();
        }
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.SetVelocityX(0);
        enemy.SetVelocityY(0);
    }
}