using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpCube;                // The powerUp prefab to be spawned.
    public float spawnTime = 100f;            // How long between each spawn.

    void Start ()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        float randomNum = Random.value;
        Vector3 position = new Vector3(20f, 30f, 40f);        

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate (powerUpCube, position, Quaternion.identity);
    }

}