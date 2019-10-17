using UnityEngine;
using UnityEngine.AI;
 
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _nav;
    // public GameObject destroyExplosionPrefab;
    private Transform _player;
    public GameObject destroyExplosionPrefab;
    // Audio Source 
    private AudioSource explosionAudioSrc;
 
    void Start ()

    {
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        explosionAudioSrc = GetComponent<AudioSource>();
    }
     
    void Update ()
    {
        _nav.SetDestination(_player.position);
    }

    // // This should be hooked up to the health manager on this object
    public void DestroyMe()
    {   
        // Create explosion sound
        explosionAudioSrc.Play();

        // Create explosion effect
        GameObject explosion = Instantiate(this.destroyExplosionPrefab);
        explosion.transform.position = this.transform.position;

        // Destroy self
        Destroy(this.gameObject);
    }
}
