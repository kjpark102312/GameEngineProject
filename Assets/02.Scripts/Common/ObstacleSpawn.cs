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

    private int obstacleCost = 50;

    void Update()
    {
        ClickPoint();
    }

    void ClickPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.DrawRay(mousePos, transform.forward, Color.red, Mathf.Infinity);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

                if(hit.collider == null) return;
                    
                if (hit.transform.CompareTag("Point") && points.Count == 0) 
                {
                    points.Add(hit.transform);
                }
                else if (hit.transform.CompareTag("Point"))
                {
                    points.Add(hit.transform);

                    Debug.Log("�ι�° Ʈ������");

                    obstacleSpawner(points[0], points[1]);
                }
                Debug.Log("�Ȱ��� ���� ��ֹ��� ��ġ�� �� �����ϴ�!!!");
            }
        }

    }

    void obstacleSpawner(Transform firstPointTr, Transform lastPointTr)
    {
        if(GameManager.Instance.gold > obstacleCost)
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

            Instantiate(horizontalObj, firstPointTr.position, ObstacleRotate(obstacleDir));

            points.RemoveAt(0);
            points.RemoveAt(0);

            GameManager.Instance.gold -= obstacleCost;
        }
        else
        {
            return;
        }
    }

    public Quaternion ObstacleRotate(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
