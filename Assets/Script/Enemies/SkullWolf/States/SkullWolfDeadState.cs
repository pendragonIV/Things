using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfDeadState : SkullWolfState
{
    public SkullWolfDeadState(SkullWolf skullWolf, SkullWolfStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
    {
    }
}
