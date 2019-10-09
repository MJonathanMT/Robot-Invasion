using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1.0f;
    public BulletController bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseScreenPos = Input.mousePosition;

           
            float distanceFromCameraToXZPlane = Camera.main.transform.position.y;

            
            Vector3 screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
            Vector3 fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);

           
            BulletController p = Instantiate<BulletController>(bulletPrefab);
            p.transform.position = this.transform.position;
            p.velocity = (fireToWorldPos - this.transform.position).normalized * 10.0f;
        }
    }
}
