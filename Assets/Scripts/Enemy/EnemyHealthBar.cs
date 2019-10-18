using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image foregroundImage;

    private float updateSpeedSeconds = 0.5f;

    private void Awake(){
        GetComponentInParent<EnemyHealth>().OnHealthPercentChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float percent, int bulletDamage){
        StartCoroutine(ChangeToPercent(percent, bulletDamage));
    }

    // Interpolate damage dealt so heal decreases gradually
    private IEnumerator ChangeToPercent(float percent, int bulletDamage){
        float preChangePercent = foregroundImage.fillAmount;
        float elapsed = 0f;
        while(elapsed <updateSpeedSeconds){
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsed/updateSpeedSeconds);
            yield return null;
        }
        foregroundImage.fillAmount = percent;
        HealthManager healthManager = GetComponentInParent<HealthManager>();            
        healthManager.ApplyDamage(bulletDamage);
        if (percent == 0){
            StopCoroutine( ChangeToPercent(percent, bulletDamage) );
        }
    }

    private void LateUpdate(){
        transform.rotation =  Quaternion.Euler (new Vector3(0,90,0));
    }
}
