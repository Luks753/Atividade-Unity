using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public GameObject boss;
    float nextSpawn = 2f;
    public float spawnRate = 2f;
    int allSpawn;
    int maxSpawn = 15;
    int currentSpawn;
    bool bossNotSpawn;
    // Start is called before the first frame update
    void Start()
    {
        bossNotSpawn = true;
        currentSpawn = 0;
        allSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawn){
            int randEnemy = Random.Range(0, enemies.Length);
            int randPoint = Random.Range(0, spawnPoints.Length);

            if(allSpawn < 10){
                Instantiate(enemies[0], spawnPoints[randPoint].position, transform.rotation);
                nextSpawn = Time.time + 6f / spawnRate;
                allSpawn++;
            }else if(allSpawn <= 19){
                Instantiate(enemies[randEnemy], spawnPoints[randPoint].position, transform.rotation);
                nextSpawn = Time.time + 8f / spawnRate;                
                allSpawn++;                
            }else if(bossNotSpawn){
                Instantiate(boss, spawnPoints[randPoint].position, transform.rotation);
                bossNotSpawn = false;
            }    
            
        }
            
    }
}
