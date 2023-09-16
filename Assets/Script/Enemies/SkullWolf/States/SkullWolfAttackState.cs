using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfAttackState : SkullWolfState
{

    private bool isDetectingPlayer;
    private bool isOutOfRange;
    

    public SkullWolfAttackState(SkullWolf skullWolf, SkullWolfStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
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
        isDetectingPlayer = true;
        isOutOfRange = false;
    }

    public override void Exit()
    {
        base.Exit();
        isDetectingPlayer = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isDetectingPlayer = skullWolf.checkPlayerInRange();

        if(isDetectingPlayer)
        {
            if(skullWolf.DistanceFromSpawn() >= 5)
            {
                isOutOfRange = true;
            }

        }
        else
        {
            isOutOfRange = true;
        }

        if(isOutOfRange && skullWolf.DistanceFromSpawn() <= 1f)
        {
            skullWolfStateMachine.ChangeState(skullWolf.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isDetectingPlayer && !isOutOfRange)
        {
            if (skullWolf.DistanceFromPlayer() >= skullWolfData.attackRange)
            {
                skullWolf.MoveToPlayer();
                skullWolf.spriteRenderer.flipX = skullWolf.CheckXPlayer();
            }
        }
        else
        {
            skullWolf.MoveToSpawn();
            skullWolf.spriteRenderer.flipX = skullWolf.CheckXSpawn();
        }
    }
}

