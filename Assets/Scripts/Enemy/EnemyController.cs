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

    public EnemyBulletController enemyBulletPrefab;
    private float nextTimeToFire = 1f;
    private float timer;
    private float fireRate = 5f;
    public int attackRange;
 
    void Start ()

    {
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        explosionAudioSrc = GetComponent<AudioSource>();
    }
     
    void Update ()
    {
        _nav.SetDestination(_player.position);

        // shooting player
        if ((Vector3.Distance(_player.transform.position, this.transform.position) < attackRange) && (Time.time >= nextTimeToFire)) {
            timer = 0.0f;
            nextTimeToFire = Time.time + 1f / fireRate;
            shootBullet();
        }
    }

    // shooting bullets
    public void shootBullet()
    {
        EnemyBulletController p = Instantiate<EnemyBulletController>(enemyBulletPrefab);
        p.transform.position = this.transform.position + new Vector3( 1f,1f,1f);
        p.velocity = (_player.transform.position - this.transform.position).normalized * 10.0f;
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
