using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerProps", menuName = "PlayerData/Base Data")] 
public class PlayerProps : ScriptableObject
{
    [Header("Base State")]
    public float walkSpeed = 1f;
    public float diagonalWalkSpeed = Convert.ToSingle(Math.Sqrt(2) / 2);

    [Header("Dash State")]
    public float dashSpeed = 1.5f;
}
