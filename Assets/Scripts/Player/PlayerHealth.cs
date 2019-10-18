using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    public int currentHealth;
    public event Action<float, int> OnHealthPercentChanged;
    public float currentHealthPercent;

    private void OnEnable(){
        currentHealth = maxHealth;
    }
    public void ModifyHealth(int amount){
        currentHealth -= amount;
        currentHealthPercent = (float)currentHealth/(float)maxHealth;
        OnHealthPercentChanged(currentHealthPercent, amount);
    }

    // Update is called once per frame
    void Update()
    {
        // if(collision with bullets decrease health)
      
        
    }

     void OnCollisionEnter(Collision c) {

        if (c.collider.name == "EnemyProjectile") {
            ModifyHealth(5);
        }
    }
}
