using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image foregroundImage;

    private float updateSpeedSeconds = 0.5f;

    private void Awake(){
        GetComponentInParent<PlayerHealth>().OnHealthPercentChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float percent){
        StartCoroutine(ChangeToPercent(percent));
    }

    // Interpolate damage dealt so heal decreases gradually
    private IEnumerator ChangeToPercent(float percent){
        float preChangePercent = foregroundImage.fillAmount;
        float elapsed = 0f;
        while(elapsed <updateSpeedSeconds){
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsed/updateSpeedSeconds);
            yield return null;
        }
        foregroundImage.fillAmount = percent;
    }

    private void LateUpdate(){
        transform.LookAt(Camera.main.transform);
    }
}
