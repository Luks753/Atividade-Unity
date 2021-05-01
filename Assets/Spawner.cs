using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    float nextSpawn = 5f;
    public float spawnRate = 2f;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawn){
            int randPoint = Random.Range(0, spawnPoints.Length);
            if(score < 10){
                Instantiate(enemies[0], spawnPoints[randPoint].position, transform.rotation);
            }    
            nextSpawn = Time.time + 5f / spawnRate;
        }
            
    }
}
