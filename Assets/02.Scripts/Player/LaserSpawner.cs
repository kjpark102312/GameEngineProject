using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject laserTrPanel;
    [SerializeField]
    private GameObject laserTower;
    [SerializeField]
    private int laserTowerCost = 100;


    bool isOnPanel;
    public void ShowLaserSpawnTr()
    {
        if (laserTrPanel.activeSelf == true) laserTrPanel.SetActive(false);
        else if (laserTrPanel.activeSelf == false) laserTrPanel.SetActive(true);

        //bool s = laserTrPanel.activeSelf ? true : false;
        //laserTrPanel.SetActive(s);
    }

    public void BuyLaserTower()
    {
        if (GameManager.Instance.gold > laserTowerCost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Debug.DrawRay(mousePos, transform.forward, Color.red, Mathf.Infinity);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

                    //bool isHit = 

                    if (hit.transform.CompareTag("TowerPos"))
                    {
                        Instantiate(laserTower, hit.transform.position, Quaternion.identity);
                    }
                }
            }
            
        }
        else
        {
            return;
        }
    }
}
