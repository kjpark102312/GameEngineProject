using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemySpawner : MonoBehaviour
{
    private Wave currentWave;

    public List<GameObject> enemyList;


    private int allEnemyCount = 0;
    private int spawnCSEnemyCount = 0;
    private int spawnTSEnemyCount = 0;
    private int spawnSSEnemyCount = 0;
    private void Start()
    {
        StartWave(currentWave);
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;

        allEnemyCount = 0;

        StartCoroutine(CSEnemySpawn());
        StartCoroutine(TSEnemySpawn());
        StartCoroutine(SSEnemySpawn());
    }

    public IEnumerator CSEnemySpawn()
    {
        while (allEnemyCount < currentWave.maxEnemyCount)
        {
            Instantiate(currentWave.enemyPrefabs[0], transform.position, Quaternion.identity);


            yield return new WaitForSeconds(currentWave.CSspawnTime);

            spawnCSEnemyCount++;
            allEnemyCount++;

            if (spawnCSEnemyCount % currentWave.CSwaitEnemyCount == 0)
            {
                yield return new WaitForSeconds(currentWave.waitTime);
            }
        }
    }
    public IEnumerator TSEnemySpawn()
    {
        if (currentWave.enemyPrefabs[1] != null)
        {
            while (allEnemyCount < currentWave.maxEnemyCount)
            {
                yield return new WaitForSeconds(currentWave.TSspawnTime);

                Instantiate(currentWave.enemyPrefabs[1], transform.position, Quaternion.identity);

                spawnTSEnemyCount++;
                allEnemyCount++;
            }
        }
    }
    public IEnumerator SSEnemySpawn()
    {

        if(currentWave.enemyPrefabs[2] != null)
        {
            while (allEnemyCount < currentWave.maxEnemyCount)
            {
                yield return new WaitForSeconds(currentWave.SSspawnTime);
                
                Instantiate(currentWave.enemyPrefabs[2], transform.position, Quaternion.identity);

                spawnSSEnemyCount++;
                allEnemyCount++;
            }
        }
    }
}


