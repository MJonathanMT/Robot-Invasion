using UnityEngine;
using UnityEngine.AI;
 
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _nav;
    private PlayerController player;
    public BulletController enemyBulletPrefab;
    private float nextTimeToFire = 1f;
    private float timer;
    private float fireRate = 5f;
    public int attackRange;

    void Start ()
    {
        _nav = GetComponent<NavMeshAgent>();
        // Get player reference if none attached already
        if (this.player == null)
        {
            this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
     
    void Update ()
    {
        _nav.SetDestination(player.transform.position);

        if ((Vector3.Distance(player.transform.position, this.transform.position) < attackRange) && (Time.time >= nextTimeToFire)) {
            timer = 0.0f;
            nextTimeToFire = Time.time + 1f / fireRate;
            shootBullet();
        }
    }

    public void shootBullet()
    {
        BulletController p = Instantiate<BulletController>(enemyBulletPrefab);
        p.transform.position = this.transform.position + new Vector3( 1f,1f,1f);
        p.velocity = (player.transform.position - this.transform.position).normalized * 10.0f;
    }
}