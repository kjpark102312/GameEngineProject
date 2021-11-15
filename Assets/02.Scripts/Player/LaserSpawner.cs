using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LaserSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject laserTrPanel;
    [SerializeField]
    private GameObject laserTower;
    private int laserTowerCost = 100;

    [SerializeField]
    private Text laserTowerCostText;

    public GameObject laserBulidTr;

    private RaycastHit2D hit;

    public Vector2 laserDir;


    public void ShowLaserSpawnTr()
    {
        if (laserTrPanel.activeSelf == true) laserTrPanel.SetActive(false);
        else if (laserTrPanel.activeSelf == false) laserTrPanel.SetActive(true);

        
    }
    private void Start()
    {
        laserTowerCostText.text = $"{laserTowerCost}";
    }

    private void Update() 
    {
        BuyLaserTower();
    }

    void CostCalculation()
    {
        laserTowerCostText.text = $"{laserTowerCost}";
        GameManager.Instance.gold -= laserTowerCost;
        laserTowerCost = (int)(((laserTowerCost / 2) + laserTowerCost) * 0.1f) * 10;
        laserTowerCostText.text = $"{laserTowerCost}";
    }
    public void BuyLaserTower()
    {
        if (GameManager.Instance.gold > laserTowerCost && GameManager.Instance.towerBulidCount > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.DrawRay(mousePos, transform.forward, Color.red, Mathf.Infinity);

                hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

                if(hit.collider == null) return;

                if (hit.transform.CompareTag("TowerPos"))
                {
                    GameObject tower =  Instantiate(laserTower, hit.transform.position, LaserTowerRotate());
                    tower.transform.parent = laserBulidTr.transform;
                    Debug.Log(hit.transform.gameObject.transform.right);
                    hit.transform.gameObject.SetActive(false);
                    
                    GameManager.Instance.towerBulidCount--;
                    CostCalculation();

                    laserDir = hit.transform.gameObject.transform.up;
                }
            }
        }
        else
        {
            return;
        }
    }

    public Quaternion LaserTowerRotate()
    {
        switch (hit.transform.parent.name)
        {
            case "Top":
                Debug.Log("설치");
                return laserTower.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            case "Bottom":
                Debug.Log("설치");
                return laserTower.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            case "Right":
                Debug.Log("설치");
                return laserTower.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            case "Left":
                Debug.Log("설치");
                return laserTower.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            default:
                return laserTower.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
