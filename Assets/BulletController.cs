using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0,0,25);

    // Update is called once per frame
    void Update () {
        this.transform.Translate(velocity * Time.deltaTime);
	}
}
