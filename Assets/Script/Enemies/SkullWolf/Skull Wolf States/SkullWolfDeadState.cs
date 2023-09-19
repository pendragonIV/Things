using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfDeadState : EnemyState
{
    public SkullWolfDeadState(SkullWolf skullWolf, EnemyStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
    {
    }
}
