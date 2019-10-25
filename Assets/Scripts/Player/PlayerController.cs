using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource shootingAudioSrc;
    public AudioSource reloadingAudioSrc;

    public float speed = 1.0f;
    public BulletController bulletPrefab;
    private Vector2 mouseScreenPos;
    private Vector3 screenPosWithZDistance;
    private Vector3 fireToWorldPos;
    private Vector2 positionOnScreen ;
    private Vector2 mouseOnScreen ;
    private float angle;
    private Vector3 lDirection;
    private Vector3 rightArmPosition = new Vector3(0.01999995f,0,0);
    private Quaternion bulletAngle;
    
    private float forwardDirection;
    private float sideDirection;
    private Vector3 forwardVelocity;
    private Vector3 sideVelocity;
    private Vector3 currentVelocity;

    private float reloadTime = 1.5f;
    private float timer = 0;
    private bool timerReached = false;
    private float nextTimeToFire = 0f;
    private float fireRate = 15f;
    private Animator anim;
    private Vector3 pos;

    private ParticleSystem PowerUpParticleSystem;
    private float powerPickUpRange = 5;

    private float tempAmmo;
    public GameObject destroyExplosionPrefab;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        // Get rigidbody and set starting position
        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(25f,0,40f);
        transform.rotation =  Quaternion.Euler (new Vector3(0f,90f,0));
			anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);         
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen)*-1+180;
        // rotate player to face mouse
        transform.rotation =  Quaternion.Euler (new Vector3(0f,angle,0));
        
        lDirection =new  Vector3( Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));

         // Getting Keyboard Inputs
        forwardDirection = Input.GetAxis("Vertical");
        sideDirection = Input.GetAxis("Horizontal");
        // Move the player
        forwardVelocity = Vector3.right * forwardDirection * speed * Time.deltaTime * -1;
        sideVelocity = Vector3.forward * sideDirection * speed * Time.deltaTime;
        
        currentVelocity = forwardVelocity + sideVelocity;
        rb.velocity = Vector3.zero;
        rb.position = rb.position + currentVelocity;
        rb.position = new Vector3(rb.position.x, 0, rb.position.z);
        
        pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, 1f, 39f); 
        pos.z = Mathf.Clamp(pos.z,1f, 79f);   
        rb.position = pos;

         if (Input.anyKey){
            anim.SetInteger ("AnimationPar", 1);
         }else {
				anim.SetInteger ("AnimationPar", 0);
			}


        PlayerReload playerReload = GetComponent<PlayerReload>();
        
        if (Input.GetKey(KeyCode.R)){
            reloadingAudioSrc.Play();

            tempAmmo = playerReload.currentAmmo;
            playerReload.currentAmmo = 0;
            playerReload.ReloadAction((int) tempAmmo);
        }
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {  
            timer = 0.0f;
            nextTimeToFire = Time.time + 1f/fireRate;
            if(playerReload.currentAmmo > 0){    
                // play shooting sound
                shootingAudioSrc.Play();

                mouseScreenPos = Input.mousePosition;           
                float distanceFromCameraToXZPlane = Camera.main.transform.position.y;
        
                screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
                fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);
                fireToWorldPos.y = 0;
            
                BulletController p = Instantiate<BulletController>(bulletPrefab);       
                
                GameObject firePoint = this.transform.Find("FirePoint").gameObject;     
                p.transform.position = firePoint.transform.position;            
                p.velocity = (lDirection).normalized *10.0f ;

                // Uncomment when merge together
                GameObject pBullet = p.transform.Find("Projectile").gameObject;
                pBullet.transform.eulerAngles = new Vector3(pBullet.transform.eulerAngles.x, angle, pBullet.transform.eulerAngles.z );
            
                playerReload.ModifyAmmo(-1);
            }
            
        }
        
        PowerUpParticleSystem = GameObject.Find("PowerUpParticleSystem").GetComponent<ParticleSystem>();
        
        if(Input.GetKey(KeyCode.E) && (Vector3.Distance(new Vector3(20,0,40), this.transform.position) < powerPickUpRange)){
            if(PowerUpParticleSystem.enableEmission){
                PowerUpParticleSystem.enableEmission = false;
                GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
                foreach(GameObject powerUp in powerUps)
                    GameObject.Destroy(powerUp);
                // Do power up
                applyRandomPowerUp();
            }
        }
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    public void DestroyMe()
    {   
        // Create explosion sound
        // explosionAudioSrc.Play();

        // Create explosion effect
        GameObject explosion = Instantiate(this.destroyExplosionPrefab);
        explosion.transform.position = this.transform.position;

        // Destroy self
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision c) {
        if (c.collider.tag == "Enemy") {
            PlayerHealth playerHealth = this.gameObject.GetComponent<PlayerHealth>();
            playerHealth.ModifyHealth(10);
        }
    }
    void applyRandomPowerUp(){
        float randomNum = Random.value;
        // Debug.Log(randomNum);
        Vector3 position;
        if (randomNum <= 0.1){
            Damage all enemies power up
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies){
                HealthManager healthManager = enemy.GetComponent<HealthManager>();            
                healthManager.ApplyDamage(100);
            }            
        }
        else if (randomNum <= 0.4 && randomNum > 0.1){
            // Infinite Ammo Power Up            
            PlayerReload playerReload = GetComponent<PlayerReload>();
            playerReload.maxAmmo = 300;
            playerReload.ReloadAction(300);
            fireRate = 1000000f;
            ParticleSystem infAmmoParticleSystem = this.transform.Find("InfAmmo").GetComponent<ParticleSystem>();
            infAmmoParticleSystem.enableEmission = true;
            Invoke("normalReload", 4);
        }
        else if (randomNum <= 0.7 && randomNum > 0.4){
            // Health Buff Power Up        
            PlayerHealth playerHealth = this.gameObject.GetComponent<PlayerHealth>();
            playerHealth.maxHealth = 10000;
            playerHealth.OnEnable();
            playerHealth.ModifyHealth(0);
            Invoke("normalHealth", 4);
        }
        else {
            // Score Multiplier Power Up
            ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            scoreManager.killValue *= 2;
            
            InGameController inGameController = GameObject.Find("InGameController").GetComponent<InGameController>();
            inGameController.multiplier = true;
            Invoke("normalScore", 20);
        }
    }
    void normalReload(){        
        PlayerReload playerReload = this.gameObject.GetComponent<PlayerReload>();
        fireRate = 15f;
        playerReload.maxAmmo = 4;
        playerReload.ReloadAction(4);
        ParticleSystem infAmmoParticleSystem = this.transform.Find("InfAmmo").GetComponent<ParticleSystem>();
        infAmmoParticleSystem.enableEmission = false;
    }
    void normalHealth(){
        PlayerHealth playerHealth = this.gameObject.GetComponent<PlayerHealth>();
        playerHealth.maxHealth = 100;
        playerHealth.OnEnable();
        playerHealth.ModifyHealth(0);
    }
    
    void normalScore(){
        
        ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        scoreManager.killValue = 100;
        
        InGameController inGameController = GameObject.Find("InGameController").GetComponent<InGameController>();
        inGameController.multiplier = false;
    }
}
