using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gold = 1000;
    public int currentWaveIndex = 1;

    public const float Max_Time = 100;
    public float leftTime;


    private static GameManager instance = null;
    public static GameManager Instance
    { 
        get
        {
            if (instance == null)
            {
                Debug.Log("instance�� null�Դϴ�.");

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
            Debug.Log("�ߺ��� instance �Դϴ�.");
            Destroy(this);
            return;
        }

        Instance = this;
    }

}
