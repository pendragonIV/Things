using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "EnemiesData")]

public class EnemiesDataSO : ScriptableObject
{
    [field: SerializeField]
    public float maxHealth { get; private set; }
    [field: SerializeField]
    public float moveSpeed { get; private set; }
    [field: SerializeField]
    public float attackDamage { get; private set; }
    [field: SerializeField]
    public float attackRange { get; private set; }

    [field: SerializeField]
    public LayerMask whatIsPlayer { get; private set; }

    [field: SerializeField]
    public float detectRange { get; private set; }

}
