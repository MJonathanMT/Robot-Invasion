using UnityEngine;
using UnityEngine.AI;
 
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _nav;
    // public GameObject destroyExplosionPrefab;
    private Transform _player;
    public GameObject destroyExplosionPrefab;
    // Audio Source 
    public AudioSource explosionAudioSource;
    public AudioSource shootingAudioSource;

    public EnemyBulletController enemyBulletPrefab;
    private float nextTimeToFire = 1f;
    private float timer;
    private float fireRate = 5f;
    public int attackRange;
 
    void Start ()

    {
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
     
    void Update ()
    {
        _nav.SetDestination(_player.position);

        // shooting player
        if ((Vector3.Distance(_player.transform.position, this.transform.position) < attackRange) && (Time.time >= nextTimeToFire)) {
            timer = 0.0f;
            nextTimeToFire = Time.time + 2f / fireRate;
            shootBullet();
            // Create shooting sound
            shootingAudioSource.Play();
        }
    }

    // Shooting bullets
    public void shootBullet()
    {
        EnemyBulletController p = Instantiate<EnemyBulletController>(enemyBulletPrefab);
        p.transform.position = this.transform.position + new Vector3(1f,1f,1f);
        p.velocity = (_player.transform.position - this.transform.position).normalized * 15.0f;
    }

    // // This should be hooked up to the health manager on this object
    public void DestroyMe()
    {
        // Create explosion sound
        explosionAudioSource.Play();

        // Create explosion effect
        GameObject explosion = Instantiate(this.destroyExplosionPrefab);
        explosion.transform.position = this.transform.position;

        // Destroy self
        Destroy(this.gameObject);
    }
}
