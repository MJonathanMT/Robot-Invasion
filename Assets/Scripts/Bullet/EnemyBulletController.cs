using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public string tagToDamage;
    public Vector3 velocity = new Vector3(0,0,25);
    public int bulletDamage = 1;

    // Update is called once per frame
    void Update () {
        this.transform.Translate(velocity * Time.deltaTime);
	}

    void OnCollisionEnter (Collision col){
        if (col.gameObject.tag == tagToDamage)
        {
            // Damage object with relevant tag
             // Damage object with relevant tag
            PlayerHealth playerHealth = col.gameObject.GetComponent<PlayerHealth>();
            playerHealth.ModifyHealth(bulletDamage);
        }
        velocity = new Vector3(0,0,0);
        Destroy (this.gameObject); 
    }
}
