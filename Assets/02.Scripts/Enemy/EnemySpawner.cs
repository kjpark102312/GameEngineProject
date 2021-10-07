using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemySpawner : MonoBehaviour
{
    private Wave currentWave;

    public GameObject enemyPrefabs;


    public void StartWave(Wave wave)
    {
        currentWave = wave;

        StartCoroutine(EnemySpawn());
        
    }

    public IEnumerator EnemySpawn()
    {
        while (true)
        {
            Instantiate(enemyPrefabs, transform.position, Quaternion.identity);

            //yield return new WaitForSeconds();    
        }
    }
}


