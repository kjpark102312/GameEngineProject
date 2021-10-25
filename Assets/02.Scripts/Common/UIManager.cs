using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text waveText;

    private int currentWaveIndex;

    

    void Start()
    {
        currentWaveIndex = GameManager.Instance.currentWaveIndex;    
    }

    void Update()
    {
        WaveStart();
    }

    void WaveStart()
    {
        waveText.text = $"WAVE {currentWaveIndex}";
    }

    
}
