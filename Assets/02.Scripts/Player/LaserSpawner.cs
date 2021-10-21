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
    private int laserTowerCost = 100;

    private RaycastHit2D hit;

    bool isOnPanel;

    public Vector2 laserDir;

    public void ShowLaserSpawnTr()
    {
        if (laserTrPanel.activeSelf == true) laserTrPanel.SetActive(false);
        else if (laserTrPanel.activeSelf == false) laserTrPanel.SetActive(true);

        //bool s = laserTrPanel.activeSelf ? true : false;
        //laserTrPanel.SetActive(s);
    }

    private void Update() 
    {
        BuyLaserTower();
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

                    hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

                    if(hit.collider == null) return;

                    if (hit.transform.CompareTag("TowerPos"))
                    {
                        Instantiate(laserTower, hit.transform.position, Quaternion.identity);
                        hit.transform.gameObject.SetActive(false);
                        LaserDir(hit);
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    public void LaserDir(RaycastHit2D hit2D)
    {
        switch(hit.transform.parent.name)
        {
            case "Top":
            hit2D = Physics2D.Raycast(transform.position, transform.up* -1, 100f);
            laserDir = transform.up * -1;
            break;
            case "Bottom":
            hit2D = Physics2D.Raycast(transform.position, transform.up, 100f);
            laserDir = transform.up;
            break;
            case "Right":
            hit2D = Physics2D.Raycast(transform.position, transform.right * -1, 100f);    
            laserDir = transform.right * -1;
            break;
            case "Left":
            hit2D = Physics2D.Raycast(transform.position, transform.right, 100f); 
            laserDir = transform.right;   
            break;
        }
    }
}
