using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gold = 1000;
    public int currentWaveIndex = 0;

    public const float Max_Time = 100;
    public float leftTime;

    public int maxHp = 15;
    public int currentHp = 15;

    public int towerBulidCount = 3;

    private static GameManager instance = null;
    public static GameManager Instance
    { 
        get
        {
            if (instance == null)
            {
                Debug.Log("instance가 null입니다.");

                return null;
            }

            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("중복된 instance 입니다.");
            Destroy(this);
            return;
        }

        Instance = this;
    }

}
