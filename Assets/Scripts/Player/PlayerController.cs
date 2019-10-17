using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1.0f;
    public BulletController bulletPrefab;
    private AudioSource shootingAudioSrc;
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
    bool timerReached = false;
    private float nextTimeToFire = 0f;
    private float fireRate = 15f;
		private Animator anim;

    private float tempAmmo;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // shooting audio source
        shootingAudioSrc = GetComponent<AudioSource>();

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

         if (Input.anyKey){
            anim.SetInteger ("AnimationPar", 1);
         }else {
				anim.SetInteger ("AnimationPar", 0);
			}


        PlayerReload playerReload = GetComponent<PlayerReload>();
        
        if (Input.GetKey(KeyCode.R)){
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
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
