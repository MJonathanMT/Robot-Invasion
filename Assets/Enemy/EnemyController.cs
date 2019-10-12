using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        // get position of target object
        Vector3 targetPosition = player.transform.position;

        // gives us vector to direction of target
        Vector3 inverseVect = transform.InverseTransformPoint(targetPosition);

        // calculate angle by which you have to rotate
        // Note -: This angle is calculated every Frame of FixedUpdate
        float rotationAngle = Mathf.Atan2(inverseVect.x, inverseVect.z) * Mathf.Rad2Deg;

        // Now calculate  rotationVelocity to be applied every frame
        Vector3 rotationVelocity = (Vector3.up * rotationAngle) * 10.0f * Time.deltaTime;

        // Calaculate his delta velocity   i.e required - current 
        Vector3 deltavel = (rotationVelocity - rb.angularVelocity);

        // Apply the force to rotate
        rb.AddTorque(deltavel, ForceMode.Impulse);
    }
}
