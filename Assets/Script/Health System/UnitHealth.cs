using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField]
    private float currentMaxHealth;
    [SerializeField]
    private float health;

    [SerializeField]
    private UnityEvent OnDie;

    public Action<float, float> OnChange;


    public float CurrentHealth { 
        get => health;
        set { 
            health = value; 
            OnChange?.Invoke(health, currentMaxHealth);
        }
    }

    public float CurrentMaxHealth
    {
        get => currentMaxHealth;
        set
        {
            currentMaxHealth = value;
            OnChange?.Invoke(health, CurrentMaxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDie.Invoke();
        }
    }

    public void Heal(float heal)
    {
        CurrentHealth += heal;
        if(CurrentHealth > currentMaxHealth)
        {
            CurrentHealth = currentMaxHealth;
        }
    }
}
