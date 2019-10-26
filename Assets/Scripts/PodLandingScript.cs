using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodLandingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pod;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.transform.position.y != 0) {
            pod.transform.Translate(new Vector3(0.0f, -5.0f, 0.0f));
        }
    }
}
