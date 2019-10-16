using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerReload : MonoBehaviour
{
    public int maxAmmo = 4;
    public int currentAmmo;
    public event Action<float> OnAmmoPercentChanged;
    public event Action<float> OnAmmoReload;
    public float currentAmmoPercent;
    public bool reloading = false;
    private void OnEnable(){
        currentAmmo = maxAmmo;
    }
    public void ModifyAmmo(int amount){
        currentAmmo += amount;
        currentAmmoPercent = (float)currentAmmo/(float)maxAmmo;
        OnAmmoPercentChanged(currentAmmoPercent);
    }
    public void ReloadAction(){
        new WaitForSeconds(3f);
        currentAmmo = maxAmmo;
        OnAmmoPercentChanged(100);
        reloading = false;
    }

}
