using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemySpawner : MonoBehaviour
{
    private Wave currentWave;

    public List<GameObject> enemyList;

    [SerializeField]
    private GameObject enemyHPSlider;
    [SerializeField]
    private Transform canvasTr;


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
            GameObject clone = Instantiate(currentWave.enemyPrefabs[0], transform.position, Quaternion.identity);

            SpawnEnemyHPBar(clone);

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

                GameObject clone = Instantiate(currentWave.enemyPrefabs[1], transform.position, Quaternion.identity);

                SpawnEnemyHPBar(clone);
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

                GameObject clone = Instantiate(currentWave.enemyPrefabs[2], transform.position, Quaternion.identity);
                SpawnEnemyHPBar(clone);
                spawnSSEnemyCount++;
                allEnemyCount++;
            }
        }
    }


    public void SpawnEnemyHPBar(GameObject enemy)
    {
        GameObject slider = Instantiate(enemyHPSlider);

        slider.transform.SetParent(canvasTr);
        slider.transform.localScale = Vector3.one;

        slider.GetComponent<HPBarSetter>().Setup(enemy.transform);
        slider.GetComponent<EnemyHPSlider>().Setup(enemy.GetComponent<EnemyHp>());
    }
}


