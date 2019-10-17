using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerReloadBar : MonoBehaviour
{
    public Image foregroundImage;

    // reload speed
    private float updateSpeedSeconds = 100f;

    private void Awake(){
        GetComponentInParent<PlayerReload>().OnAmmoPercentChanged += HandleAmmoChanged;
        GetComponentInParent<PlayerReload>().OnAmmoReload += HandleReload;
    }

    private void HandleAmmoChanged(float percent){
        foregroundImage.fillAmount = percent;
    }

    private void HandleReload(float percent){
        StartCoroutine(ChangeFromPercent(percent));
    }
     // Interpolate damage dealt so heal decreases gradually
    private IEnumerator ChangeFromPercent(float percent){
        
        PlayerReload playerReload = GetComponentInParent<PlayerReload>();        
        float preAmmoPercent = percent;
        float elapsed = 0f;
        while(foregroundImage.fillAmount < 1){
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preAmmoPercent, 100, elapsed/updateSpeedSeconds); 
            yield return null;   
        }
        playerReload.currentAmmo = playerReload.maxAmmo;
    }
    private void LateUpdate(){
        transform.LookAt(Camera.main.transform);
    }
}
