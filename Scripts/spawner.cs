using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class spawner : MonoBehaviour
{    


    // vars for spawning enemies
    public List<GameObject> enemies;
    private float nextEnemySpawn = 0.0f;
    private float enemySpawnRate = 8.0f;
    private const float MINENEMY_X = -2.0f;
    private const float MAXENEMY_X = 2.0f;
    private const float MAXENEMY_Z = 1.25f;
    private const float MINENEMY_Z = -1.25f;
    private const float ENEMY_Y = 0.5f;


    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var slimes = GameObject.FindGameObjectsWithTag("slime");
        if (Time.time > nextEnemySpawn && slimes.Length < 10)
        {
            nextEnemySpawn = Time.time + enemySpawnRate;
            enemySpawnRate = Random.Range(enemySpawnRate - 2.0f, enemySpawnRate + 2.0f);
            float xPos = Random.Range(MINENEMY_X, MAXENEMY_X);
            float zPos = Random.Range(MINENEMY_Z, MAXENEMY_Z);
            int enemyNum = Random.Range(0, enemies.Count);
            Instantiate(enemies[enemyNum], new Vector3(xPos, ENEMY_Y, zPos), Quaternion.identity);
        }
    }
}
