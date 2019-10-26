using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fog;
    public int fogDelay;
    public int fogFrequency;
    private Color newColor;

    void Start() {
        newColor = this.transform.GetComponent<Renderer>().material.color;
        InvokeRepeating ("showFog", fogDelay, fogFrequency);
        InvokeRepeating ("disableFog", fogDelay+4, fogFrequency+4);
    }

    void Update()
    {
        
       // showFog();
    }

    void showFog() {
        
       // if (newColor.a < 0.7) {
            newColor.a = 0.7f;
            transform.GetComponent<Renderer>().material.color = newColor;
       // }
        
        
    }

    void disableFog() {
        newColor.a = 0.0f;
        transform.GetComponent<Renderer>().material.color = newColor;
    }
    
}
