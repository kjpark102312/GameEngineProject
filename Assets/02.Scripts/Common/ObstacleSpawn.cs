using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleSpawn : MonoBehaviour
{

    public GameObject horizontalObj;
    public GameObject verticalObj;

    [SerializeField]
    private List<Transform> points;
    [SerializeField]
    private LaserSpawner laserSpawner;

    public List<LaserTower> laserTowers = new List<LaserTower>();
    public LaserTower[] laserTower;

    private int obstacleCost = 50;

    public GameObject towerParent;

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
                points.Add(hit.transform);
            }
            else if (hit.transform.CompareTag("Point"))
            {
                points.Add(hit.transform);

                obstacleSpawner(points[0], points[1]);
                laserTower = towerParent.GetComponentsInChildren<LaserTower>();

                //for(int i = 0; i < laserTower.Length; i++)
                //{
                //    laserTower[i].CheckLaserState();
                //}
            }
        }
    }

    void obstacleSpawner(Transform firstPointTr, Transform lastPointTr)
    {
        if (GameManager.Instance.gold > obstacleCost)
        {
            Points firstPoint = firstPointTr.GetComponent<Points>();
            Points secondPoint = lastPointTr.GetComponent<Points>();

            if (firstPoint.isBuildObstacle == true || secondPoint.isBuildObstacle == true)
            {
                return;
            }

            firstPoint.isBuildObstacle = true;
            secondPoint.isBuildObstacle = true;

            Vector3 obstacleDir = firstPointTr.position - lastPointTr.position;

            GameObject Obstacle = Instantiate(horizontalObj, firstPointTr.position, Quaternion.identity);
            Debug.Log(GetAngel(obstacleDir.normalized, Obstacle.transform.right));

            if (GetAngel(obstacleDir.normalized, Obstacle.transform.right) < 90 && GetAngel(obstacleDir.normalized, Obstacle.transform.right) > 0
                || GetAngel(obstacleDir.normalized, Obstacle.transform.right) < 140 && GetAngel(obstacleDir.normalized, Obstacle.transform.right) > 130)
            {
                Debug.Log(GetAngel(obstacleDir, horizontalObj.transform.forward));
                Destroy(Obstacle);
                Instantiate(horizontalObj, firstPointTr.position, Obstaclerotate(obstacleDir));

                points.RemoveAt(0);
                points.RemoveAt(0);

                GameManager.Instance.gold -= obstacleCost;
                return;
            }
            else
            {
                Destroy(Obstacle);
                Instantiate(verticalObj, firstPointTr.position, Obstaclerotate(obstacleDir));

                points.RemoveAt(0);
                points.RemoveAt(0);

                GameManager.Instance.gold -= obstacleCost;
            }
        }
        else
        {
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
        else if (angle < -135 && angle > -140) angle = -135;
        else angle = -225;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
