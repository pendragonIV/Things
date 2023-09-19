using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfAttackedState : EnemyState
{
    public SkullWolfAttackedState(SkullWolf skullWolf, EnemyStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
    {
    }
}
