using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerReloadBar : MonoBehaviour
{
    public Image foregroundImage;

    private float updateSpeedSeconds = 100f;

    private void Awake(){
        GetComponentInParent<PlayerReload>().OnAmmoPercentChanged += HandleAmmoChanged;
        GetComponentInParent<PlayerReload>().OnAmmoReload += HandleReload;
    }

    private void HandleAmmoChanged(float percent){
        foregroundImage.fillAmount = percent;
    }

    private void HandleReload(float percent){
        StartCoroutine(ChangeToPercent(percent));
    }
     // Interpolate damage dealt so heal decreases gradually
    private IEnumerator ChangeToPercent(float percent){
        
        PlayerReload playerReload = GetComponentInParent<PlayerReload>();        
        float preAmmoPercent = 0;
        float elapsed = 0f;
         Debug.Log("what is going on");
        while(elapsed <updateSpeedSeconds){
            // playerReload.reloading = true;
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preAmmoPercent, percent, elapsed/updateSpeedSeconds); 
            // playerReload.reloading = false;
        }
            yield return null;   

        // foregroundImage.fillAmount = percent;
    }
    private void LateUpdate(){
        transform.LookAt(Camera.main.transform);
    }
}
