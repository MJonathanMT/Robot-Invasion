﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);         
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen)*-1-90;
        transform.rotation =  Quaternion.Euler (new Vector3(0f,angle,0));
        lDirection =new  Vector3( Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += (Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += (Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += (Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += (Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
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
        }

    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
