using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCubeController : MonoBehaviour
{
    private ParticleSystem PowerUpParticleSystem;
    private Vector3 target = new Vector3(20f, 1f, 40f);
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        transform.Rotate (0,90*Time.deltaTime,0); //rotates 90 degrees per second around y axis

        if (transform.position.y == 1){
            PowerUpParticleSystem = GameObject.Find("PowerUpParticleSystem").GetComponent<ParticleSystem>();
            PowerUpParticleSystem.enableEmission = true;
            //  PowerUpParticleSystem.;
        }

    }
}
