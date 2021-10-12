using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemySpawner;
    private int currentWave = -1;

    private void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        if(enemySpawner.enemyList.Count == 0 && currentWave < waves.Length -1)
        {
            currentWave++;

            enemySpawner.StartWave(waves[currentWave]);
        }
    }
}



[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}