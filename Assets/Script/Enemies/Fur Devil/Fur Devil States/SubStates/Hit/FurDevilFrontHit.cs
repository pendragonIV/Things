using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurDevilFrontHit : FurDevilBaseState
{
    private bool isKnokedBack;
    public FurDevilFrontHit(Enemy enemy, EnemyStateMachine enemyStateMachine, EnemiesDataSO enemyData, string animationName) : base(enemy, enemyStateMachine, enemyData, animationName)
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
        isKnokedBack = false;
        enemy.unitHealth.TakeDamage(GameManager.instance.player.GetComponent<Player>().playerBaseDamage);
        enemy.isDead = enemy.CheckDead();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    { 
        base.LogicUpdate();
        if (!enemy.isHit)
        {
            isInAttackRange = enemy.DistanceFromPlayer() <= enemyData.attackRange;

            if (isInAttackRange)
            {
                enemyStateMachine.ChangeState(enemy.FrontAttackState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemy.FrontRunState);
            }
            
        }
        else
        {
            if (enemy.isDead)
            {
                enemyStateMachine.ChangeState(enemy.FrontDeadState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(enemy.isHit && !isKnokedBack)
        {
            enemy.KnockBack();
            enemy.StartCoroutine(enemy.KnockBackTimer());
            isKnokedBack = true;
        }
    }
}
