using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 1000;
    public int currentHealth;
    public event Action<float,int> OnHealthPercentChanged;
    public float currentHealthPercent;

    private void OnEnable() {
        currentHealth = maxHealth;
    }

    public void ModifyHealth(int amount) {
        currentHealth -= amount;
        currentHealthPercent = (float)currentHealth/(float)maxHealth;
        OnHealthPercentChanged(currentHealthPercent, amount);
    }

}
