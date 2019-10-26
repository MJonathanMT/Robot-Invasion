using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject portal2;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            player.transform.position = portal2.transform.position + new Vector3(6.0f, 0.0f, 0.0f);
            player.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }
    }
}
