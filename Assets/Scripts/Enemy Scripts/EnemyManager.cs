﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] EnemyVariants;
    public GameObject[] enemySpawnPoints;
    HUDManager hudManager;
    private int Wave;
    // Start is called before the first frame update
    void Start()
    {
        Wave = 0;
        hudManager = FindObjectOfType<HUDManager>();
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length < 1)
        {
            SpawnEnemies(1);
            Wave++;
            hudManager.roundsSurvived = Wave;
        }
    }


    private void SpawnEnemies(int x)
    {
        int m_x = x;
        for(int i = 0; i < m_x; i++)
        {

            StartCoroutine(SpawnEnemy());
        }
        
    }


    IEnumerator SpawnEnemy()
    {
        int rnd = Random.Range(0, 2);
        int rnd2 = Random.Range(0, enemySpawnPoints.Length);
        GameObject Enemy = Instantiate(EnemyVariants[rnd], enemySpawnPoints[rnd2].transform.position, enemySpawnPoints[rnd2].transform.rotation);
        yield return new WaitForSeconds(0.5f);
        
    }

}


