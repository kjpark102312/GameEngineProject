using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject laserTrPanel;
    [SerializeField]
    private GameObject laserTower;

    private int laserTowerCost = 100;

    bool isOnPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ShowLaserSpawnTr()
    {
        if (laserTrPanel.activeSelf == true) laserTrPanel.SetActive(false);
        else if (laserTrPanel.activeSelf == false) laserTrPanel.SetActive(true);
    }

    public void BuyLaserTower(GameObject button)
    {
        if(GameManager.Instance.gold > laserTowerCost)  
        {
            Instantiate(laserTower, button.transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }
    }
}
