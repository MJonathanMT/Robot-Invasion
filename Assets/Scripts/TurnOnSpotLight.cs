using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSpotLight : MonoBehaviour
{
    public GameObject sun;
    public Light spotlight;
    // Start is called before the first frame update
    void Start()
    {
         Debug.Log(sun.transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (sun.transform.localEulerAngles.z < 300 && sun.transform.localEulerAngles.z > 80)
        {
           
            spotlight.enabled = true;
        }

        else {
            spotlight.enabled = false;
        }
    }
}
