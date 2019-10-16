using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject character;
    public int width = 1920;
    public int height = 500;
    // Start is called before the first frame update
    void Start()
    {
        for (int y=0; y<height; ++y)
       {
           for (int x=0; x<width; ++x)
           {
               Instantiate(character, new Vector3(x,y,0), Quaternion.identity);
           }
       }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
