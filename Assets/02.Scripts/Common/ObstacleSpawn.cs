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

    private int obstacleCost = 50;

    [SerializeField]
    private Text obstacleCostText;

    public GameObject towerParent;

    [SerializeField]
    public UIManager ui;

    [SerializeField]
    private GameObject buildPointsParent;

    public Transform[] buildPoints;

    private float shortDis;

    private void Start()
    {
        obstacleCostText.text = $"{obstacleCost}";
        buildPoints = buildPointsParent.gameObject.GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        ClickPoint();
    }

    void ClickPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(mousePos, transform.forward, Color.red, Mathf.Infinity);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

            if (hit.collider == null) return;

            if (hit.transform.CompareTag("Point") && points.Count == 0)
            {
                points.Add(hit.transform.gameObject.transform);
                Debug.Log("sadasd");
            }
        }   
        
        if (points.Count != 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

            Debug.LogError(buildPoints.Length);
            if(hit)
            {
                shortDis = Vector2.Distance(hit.transform.position, buildPoints[0].transform.position);
                for (int i = 0; i < buildPoints.Length - 1; i++)
                {
                    float distance = Vector2.Distance(hit.transform.position, buildPoints[i].position);

                    if (distance < shortDis)
                    {
                        nearPoint = buildPoints[i];
                        shortDis = distance;

                        if (Input.GetMouseButtonDown(0))
                        {
                            points.Add(nearPoint);
                        }
                    }
                }
            }
            
        }
        else if(points.Count == 2) obstacleSpawner(points[0], points[1]);
    }
    void CostCalculation()
    {
        obstacleCostText.text = $"{obstacleCost}";
        GameManager.Instance.gold -= obstacleCost;
        obstacleCost = (int)(((obstacleCost / 2) + obstacleCost) * 0.1f) * 10;
        obstacleCostText.text = $"{obstacleCost}";
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
