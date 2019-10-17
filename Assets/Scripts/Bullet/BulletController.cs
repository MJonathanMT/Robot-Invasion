using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string tagToDamage;
    public Vector3 velocity = new Vector3(0,0,25);

    // Update is called once per frame
    void Update () {
        this.transform.Translate(velocity * Time.deltaTime);
	}

    void OnCollisionEnter (Collision col){
        if (col.gameObject.tag == tagToDamage)
        {
            // Damage object with relevant tag
            HealthManager healthManager = col.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(100);
            Debug.Log("hit enemy");
        }
        velocity = new Vector3(0,0,0);
        Destroy (this.gameObject); 
    }
}
