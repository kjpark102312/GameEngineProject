using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private Transform wayPointGroup;
    public List<Transform> wayPoints = new List<Transform>();

    public float moveSpeed;

    public int nextIndex = 0;

   

    void Start()
    {
        wayPointGroup = GameObject.Find("WavePoints").transform;
        wayPointGroup.GetComponentsInChildren(wayPoints);
        wayPoints.RemoveAt(0);
    }

    void Update()
    {
        MoveWayPoint();

        if (Vector3.Distance(transform.position , wayPoints[nextIndex].position) < 0.02f * moveSpeed)
        {
            nextIndex++;
        }
    }

    void MoveWayPoint()
    {
        transform.position += (wayPoints[nextIndex].position - transform.position).normalized * moveSpeed * Time.deltaTime;
    }



    
}
