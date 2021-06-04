using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] EnemyVariants;
    public GameObject[] enemySpawnPoints;
    private GameObject[] enemies;
    private int counter;
    HUDManager hudManager;
    private int Wave;
    // Start is called before the first frame update
    void Start()
    {
        Wave = 1;
        hudManager = FindObjectOfType<HUDManager>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        counter = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter < 1)
        {
            StartCoroutine(SpawnEnemy(4));
            Wave++;
            hudManager.UpdateRound(Wave);
        }
    }

    IEnumerator SpawnEnemy(int _num)
    {
        print("Couroutine called");
        counter = 0;
        for (int i = 0; i < _num; i++)
        {
            int rnd = Random.Range(0, 2);
            int rnd2 = Random.Range(0, enemySpawnPoints.Length - 1);
            print("spawning enemy");
            Instantiate(EnemyVariants[rnd], enemySpawnPoints[rnd2].transform.position, enemySpawnPoints[rnd2].transform.rotation);
            counter++;
            print("spawned enemy");
            yield return new WaitForSeconds(2f);
            print("wait complete");
        }
    }


    public void EnemyDied()
    {
        counter--;
    }

}


