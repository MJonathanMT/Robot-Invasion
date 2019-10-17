using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0,1f,0);
    

    // Update is called once per frame
    void Update () {
        velocity.y = 0;
        this.transform.Translate(velocity * Time.deltaTime);
	}

    void OnCollisionEnter (Collision co){
        velocity = new Vector3(0,0,0);
       Destroy (this.gameObject);

       /* if (co.gameObject.tag == "Player")
        {
            // Damage object with relevant tag
            //HealthManager healthManager = col.gameObject.GetComponent<HealthManager>();
            //healthManager.ApplyDamage(damageAmount);

            // Destroy self
            Destroy(this.gameObject);
        } */
    }
}
