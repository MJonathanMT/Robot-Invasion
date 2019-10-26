using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public AudioSource explosion;
    public ParticleSystem targetParticleSystem;

    void Update()
    {
        if (!this.targetParticleSystem.IsAlive())
        {
            Destroy(targetParticleSystem.gameObject);
            explosion.Play();
        }
    }
}
