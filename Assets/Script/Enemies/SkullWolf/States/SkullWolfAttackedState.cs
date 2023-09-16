using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfAttackedState : SkullWolfState
{
    public SkullWolfAttackedState(SkullWolf skullWolf, SkullWolfStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
    {
    }
}
