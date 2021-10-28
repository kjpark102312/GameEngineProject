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
        //StartWave();
    }

    public void StartWave()
    {
        if(currentWave < waves.Length -1)
        {
            GameManager.Instance.currentWaveIndex++;
            currentWave++;

            enemySpawner.StartWave(waves[currentWave]);
        }
    }
}



[System.Serializable]
public struct Wave
{
    public float waitTime;
    public float CSspawnTime;
    public float TSspawnTime;
    public float SSspawnTime;
    public int CSwaitEnemyCount;
    public int TSwaitEnemyCount;
    public int SSwaitEnemyCount;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}