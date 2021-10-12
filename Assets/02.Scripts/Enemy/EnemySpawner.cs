using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemySpawner : MonoBehaviour
{
    private Wave currentWave;

    public GameObject enemyPrefabs;


    public List<GameObject> enemyList;
    private void Start()
    {
        StartWave(currentWave);
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;

        StartCoroutine(EnemySpawn());
        
    }

    public IEnumerator EnemySpawn()
    {

        int spawnEnemyCount = 0;
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            Instantiate(enemyPrefabs, transform.position, Quaternion.identity);

            enemyList.Add(enemyPrefabs);

            yield return new WaitForSeconds(currentWave.spawnTime);

            spawnEnemyCount++;
        }
    }
}


