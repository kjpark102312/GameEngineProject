using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{

    public GameObject obstacle;

    public List<Transform> points;

    //public GameObject tile;
    //private List<Tile> tiles;

    bool isBulid;

    private void Awake()
    {

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

            if(hit.transform.CompareTag("Point") && points.Count == 0)
            {
                points.Add(hit.transform);
            }
            else if (hit.transform.CompareTag("Point"))
            {
                points.Add(hit.transform);

                Debug.Log("두번째 트랜스폼");

                obstacleSpawner(points[0], points[1]);
            }   
            Debug.Log("똑같은 곳에 장애물을 설치할 수 없습니다!!!@@!@@!!!");
        }

    }

    void obstacleSpawner(Transform firstPointTr, Transform lastPointTr)
    {
        Points firstPoint = firstPointTr.GetComponent<Points>();
        Points secondPoint = lastPointTr.GetComponent<Points>();

        if (firstPoint.isBuildObstacle == true || secondPoint.isBuildObstacle == true)
        {
            return;
        }

        firstPoint.isBuildObstacle = true;
        secondPoint.isBuildObstacle = true;

        isBulid = true;

        Vector3 obstacleDir = firstPointTr.position - lastPointTr.position;

        Debug.LogError(obstacleDir);

        float angle = Mathf.Atan2(obstacleDir.y, obstacleDir.x) * Mathf.Rad2Deg;
        Instantiate(obstacle, firstPointTr.position, ObstacleRotate(obstacleDir));

        points.RemoveAt(0);
        points.RemoveAt(0);
    }

    public Quaternion ObstacleRotate(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
