using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObstacleSpawn : MonoBehaviour
{


    public GameObject horizontalObj;
    public GameObject verticalObj;

    [SerializeField]
    private List<Transform> points;
    [SerializeField]
    private Transform nearPoint;
    [SerializeField]
    private LaserSpawner laserSpawner;
    [SerializeField]
    private GameObject invisibleObstacle;



    private int obstacleCost = 50;
    private int destroyCost;

    [SerializeField]
    private Text obstacleCostText;

    public GameObject towerParent;

    [SerializeField]
    public UIManager ui;

    [SerializeField]
    private GameObject buildPointsParent;

    public Transform[] buildPoints;

    private float shortDis;

    private bool isClickPoint;
    private bool isOnDestroy;
    private void Start()
    {
        obstacleCostText.text = $"{obstacleCost}";
        buildPoints = buildPointsParent.gameObject.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        ClickPoint();
        NearPoint();
        Destroy();
    }

    void ClickPoint()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(mousePos, transform.forward, Color.red, Mathf.Infinity);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

            if (hit.collider == null) return;

            if (hit.transform.CompareTag("Point") && points.Count == 0)
            {
                points.Add(hit.transform.gameObject.transform);
                invisibleObstacle.SetActive(true);
            } 
        }
    }

    void NearPoint()
    {
        if (points.Count != 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hits = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

            if (hits)
            {
                shortDis = Vector2.Distance(hits.point, buildPoints[0].transform.position);

                for (int i = 0; i < buildPoints.Length - 1; i++)
                {
                    float distance = Vector2.Distance(hits.point, buildPoints[i].position);

                    if (distance < shortDis)
                    {
                        nearPoint = buildPoints[i];
                        Debug.DrawLine(nearPoint.position, hits.point);
                        invisibleObstacle.transform.position = points[0].transform.position;
                        invisibleObstacle.transform.rotation = Obstaclerotate(points[0].position - nearPoint.position);
                        shortDis = distance;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    invisibleObstacle.SetActive(false);
                    points.Add(nearPoint);
                    obstacleSpawner(points[0], points[1]);
                    isClickPoint = false;                                                                            
                }
            }
        }
    }

    void CostCalculation()
    {
        obstacleCostText.text = $"{obstacleCost}";
        GameManager.Instance.gold -= obstacleCost;
        destroyCost = obstacleCost / 2;
        obstacleCost = (int)(((obstacleCost / 2) + obstacleCost) * 0.1f) * 10;
        obstacleCostText.text = $"{obstacleCost}";
    }
    

    private void Destroy()
    {
        if(isOnDestroy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.DrawRay(mousePos, transform.forward, Color.red, Mathf.Infinity);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity ,1 << 6);

                Debug.Log(hit.transform.gameObject.name);

                if (hit.collider == null) return;

                if (hit.transform.CompareTag("Obstacle"))
                {
                    Debug.Log("ASD");
                    GameManager.Instance.gold += destroyCost;
                    Destroy(hit.transform.gameObject);
                    isOnDestroy = false;
                }
            }
        }
    }
    public void ClickDestroyBtn()
    {
        isOnDestroy = true;
        Debug.Log(isOnDestroy);
    }

    void obstacleSpawner(Transform firstPointTr, Transform lastPointTr)
    {
        Vector3 obstacleDir = firstPointTr.position - lastPointTr.position;
        float dis = obstacleDir.sqrMagnitude;

        if (GameManager.Instance.gold > obstacleCost && dis < 2)
        {
            Points firstPoint = firstPointTr.GetComponent<Points>();
            Points secondPoint = lastPointTr.GetComponent<Points>();

            if (firstPoint.isBuildObstacle == true || secondPoint.isBuildObstacle == true)
            {
                return;
            }

            firstPoint.isBuildObstacle = true;
            secondPoint.isBuildObstacle = true;

            GameObject Obstacle = Instantiate(horizontalObj, firstPointTr.position, Quaternion.identity);
            Debug.Log(GetAngel(obstacleDir.normalized, Obstacle.transform.right));

            if (GetAngel(obstacleDir.normalized, Obstacle.transform.right) < 90 && GetAngel(obstacleDir.normalized, Obstacle.transform.right) > 0
                || GetAngel(obstacleDir.normalized, Obstacle.transform.right) < 140 && GetAngel(obstacleDir.normalized, Obstacle.transform.right) > 130)
            {
                Destroy(Obstacle);
                Debug.Log(GetAngel(obstacleDir, horizontalObj.transform.forward));
                Instantiate(horizontalObj, firstPointTr.position, Obstaclerotate(obstacleDir));
            }
            else
            {
                Destroy(Obstacle);
                Instantiate(verticalObj, firstPointTr.position, Obstaclerotate(obstacleDir));
            }

            CostCalculation();

            points.RemoveAt(0);
            points.RemoveAt(0);
        }
        else
        {
            ui.IsBuildTower();
            points.RemoveAt(0);
            points.RemoveAt(0);
            return;
        }
    }

    public float GetAngel(Vector3 start, Vector3 end)
    {
        float dot = Vector3.Dot(start, end);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angle;
    }

    public Quaternion Obstaclerotate(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle > 0 && angle < 50) angle = 45;
        else if (angle < 0 && angle > -50) angle = -45;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
