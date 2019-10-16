using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1.0f;
    public BulletController bulletPrefab;
    public Vector2 mouseScreenPos;
    public Vector3 screenPosWithZDistance;
    public Vector3 fireToWorldPos;
     public Vector2 positionOnScreen ;
     public Vector2 mouseOnScreen ;
    public float angle;
    public Vector3 lDirection;
    public Vector3 rightArmPosition = new Vector3(0.01999995f,0,0);
    public Quaternion bulletAngle;
    
    public float forwardDirection;
    public float sideDirection;
    public Vector3 forwardVelocity;
    public Vector3 sideVelocity;
    public Vector3 currentVelocity;

    public float reloadTime = 1.5f;
    public float timer = 0;
    bool timerReached = false;
    public float nextTimeToFire = 0f;
    public float fireRate = 15f;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        // Get rigidbody and set starting position
        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(25f,0,40f);
        transform.rotation =  Quaternion.Euler (new Vector3(0f,90f,0));
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
        
        // if (Input.GetKey(KeyCode.A))
        // {
        //     this.transform.position += (Vector3.back * speed * Time.deltaTime);
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     this.transform.position += (Vector3.forward * speed * Time.deltaTime);
        // }
        // if (Input.GetKey(KeyCode.W))
        // {
        //     this.transform.position += (Vector3.left * speed * Time.deltaTime);
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     this.transform.position += (Vector3.right * speed * Time.deltaTime);
        // }
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

        PlayerReload playerReload = GetComponent<PlayerReload>();
        
        // if(Input.GetKeyDown(KeyCode.R)){
        //     timer += Time.deltaTime;
        //     if(timer > reloadTime){
        //         timer = 0;
        //         playerReload.ReloadAction();
        //     }
        // }
        if (Input.GetMouseButton(1))
        {
            timer += Time.deltaTime;
            if (timer >= reloadTime)
            {
                timer -= reloadTime;
                // foregroundImage
                playerReload.ReloadAction();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            timer = 0.0f;
        }
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {  
            timer = 0.0f;
            nextTimeToFire = Time.time + 1f/fireRate;
            if(playerReload.currentAmmo > 0){
                mouseScreenPos = Input.mousePosition;           
                float distanceFromCameraToXZPlane = Camera.main.transform.position.y;
        
                screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
                fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);
                fireToWorldPos.y = 0;

            
                BulletController p = Instantiate<BulletController>(bulletPrefab);       
                
                GameObject firePoint = this.transform.Find("FirePoint").gameObject;     
                p.transform.position = firePoint.transform.position;            
                p.velocity = (lDirection).normalized *10.0f ;

                
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
