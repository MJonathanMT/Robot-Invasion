using UnityEngine;
using System.Collections;

public class FloorShadingScript : MonoBehaviour
{
    public Shader shader;
    

    // Use this for initialization
    void Start()
    {

        GetComponent<Renderer>().material.shader = shader;
    }

    // Called each frame
    void Update()
    {
        // Get renderer component (in order to pass params to shader)
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
    }

}
