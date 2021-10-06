using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{
    public LineRenderer line;
    private LayerMask targetLayer;

    public float power = 1f;



    private void SpawnLaser()
    {
        //line.gameObject.SetActive(true);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 100f);
        Debug.DrawRay(transform.position, transform.right, Color.green, 100f);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + new Vector3(10,0,0));

        

        if(hit)
        {

            Debug.Log(hit.transform.tag);
            if (hit.transform.CompareTag("Obstacle"))
            {

                RaycastHit2D hitObstacle = Physics2D.Raycast(hit.point, Vector3.Reflect(transform.right, hit.normal), 100f);
                Debug.DrawRay(hit.point, Vector3.Reflect(transform.right, hit.normal));
            }
        }

        
        //line.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 0) + Vector3.back);

        // hit.transform.GetComponent<EnemyHp>().GetDamage(power);

    }

    void Start()
    {
        
    }

    void Update()
    {
        SpawnLaser();
    }
}
