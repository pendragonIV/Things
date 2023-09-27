using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChickenProps", menuName = "ChickenData/Base Data")]
public class ChickenProps : ScriptableObject
{

    [Header("Base State")]
    public float walkSpeed = .3f;
    public float diagonalWalkSpeed = Convert.ToSingle(Math.Sqrt(2) / 2) * 0.3f;

}
