using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text waveText;

    private int currentWaveIndex;

    [SerializeField]
    private Text timerText;

    private float second = 0;
    private int minute = 5;
    private float aSec = 0;

    private bool stop;

    [SerializeField]
    private CanvasGroup reStartPanel;
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private Text goldText;

    [SerializeField]
    private Text towerCountText;

    [SerializeField]
    private Text isBuildTowerText;
    

    void Update()
    {
        WaveStart();
        Timer();
        GetDamage();
        Gold();
        TowerBulid();
    }

    public void TowerBulid()
    {
        towerCountText.text = $"{GameManager.Instance.towerBulidCount}/3";
    }

    public void WaveStart()
    {
        if(GameManager.Instance.currentWaveIndex >= 10) waveText.text = $"WAVE {GameManager.Instance.currentWaveIndex}";
        else waveText.text = $"WAVE 0{GameManager.Instance.currentWaveIndex}";
    }

    void Timer()
    {
        if(!stop)
        {
            second -= Time.deltaTime;

            timerText.text = string.Format("남은 시간 : {0:D2}:{1:D2}", minute, (int)second);
        }
        
        if((int)second == 0)
        {
            aSec += Time.deltaTime;
            stop = true;
            if ((int)aSec == 1)
            {
                second = 60;
                minute--;
                aSec = 0;
                stop = false;
            }
        }
    }

    public void GetDamage()
    {
        hpText.text = $"{GameManager.Instance.currentHp}/{GameManager.Instance.maxHp}";

        if(GameManager.Instance.currentHp <= 0)
        {
            reStartPanel.alpha += Time.deltaTime * 1f;
            reStartPanel.blocksRaycasts = true;
            reStartPanel.interactable = true;
        }
    }

    public void IsBuildTower()
    {
        isBuildTowerText.DOFade(0.8f, 0.2f).OnComplete(() =>
        {
            isBuildTowerText.DOFade(0f, 0.2f);
        });
    }

    void Gold()
    {
        goldText.text = $" {GameManager.Instance.gold}";
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
