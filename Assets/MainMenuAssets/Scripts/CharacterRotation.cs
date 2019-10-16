using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    public float speed = 10;

    // Update is called once per frame
    public void Update()
    {
        rotateObject();
        
    }

    public void rotateObject() {
        transform.Rotate (new Vector3(0, Time.deltaTime * speed, 0));
    }
}
