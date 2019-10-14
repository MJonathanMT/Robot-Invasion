using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion iniRot;
    private float mapLength = 80f;
    private float mapDepth = 40f;
    private float cameraViewLength = 28f;
    private float cameraViewDepth = 20f;
    void Start(){
    }
    
    void Update(){
        Vector3 pos = GameObject.Find("Player").transform.position;
        this.transform.position = pos + new Vector3(8f,8f, 0);

        pos = this.transform.position;
        pos.x = Mathf.Clamp(pos.x, 23f, 43f); 
        pos.z = Mathf.Clamp(pos.z, 11f, 68f);   
        this.transform.position = pos;
    }

    // Update is called once per frame
    }
