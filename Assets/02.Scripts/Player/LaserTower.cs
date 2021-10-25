using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{

    public GameObject lineObj;
    public LineRenderer line;
    private LayerMask targetLayer;

    public LaserSpawner laserSpawner;

    public float power = 1f;

    private bool isSpawnLaser;
    private RaycastHit2D hit ;
    private RaycastHit2D hitObstacle;

    private Vector2 laserDir;

    private int laserCount = 1;

    private bool laserHit = true;

    private void Awake() 
    {
        laserSpawner = GameObject.Find("LaserSpawner").GetComponent<LaserSpawner>();
        
    }

    private void Start() 
    {
        laserSpawner.LaserDir(hit);
        laserDir = laserSpawner.laserDir;
    }

    
    void Update()
    {
        BounceLaser();
    }

    private void SpawnLaser()
    {
        Vector2 bounceVector = Vector3.Reflect(laserDir, hit.normal);

        Debug.DrawRay(hit.point, Vector3.Reflect(laserDir, hit.normal), Color.red);

        hitObstacle = Physics2D.Raycast(hit.point, bounceVector, 100f);

        line.positionCount++;
        laserCount++;
        line.SetPosition(1, hit.point);
        line.SetPosition(2, hit.transform.position + Vector3.Reflect(laserDir, hit.normal) * 10f);

        Debug.Log(line.positionCount);
        isSpawnLaser = true;

        //if(hitObstacle.transform.CompareTag("Obstacle"))
        //{
        //    line.positionCount++;
        //    laserCount++;
        //    line.SetPosition(1, hit.point);
        //    line.SetPosition(laserCount, hit.transform.position + Vector3.Reflect(laserDir, hit.normal) * 10f);
        //}
    }

    public void BounceLaser()
    {
        if(laserHit)
        {
            line.SetPosition(0, transform.position);
            hit = Physics2D.Raycast(transform.position, laserDir, 100f, 1 << 6);

            if (hit)
            {
                if (hit.transform.CompareTag("Obstacle"))
                {
                    SpawnLaser();
                    Debug.Log("방어벽");
                }
                else if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<EnemyHp>().GetDamage(power);
                    Debug.Log("적들");
                }

                Debug.Log(hit.transform.gameObject.name);
            }
            else
            {
                line.SetPosition(1, laserDir * 100);
                Debug.Log("아무것도 안맞음");
            }


            laserHit = false;   
        }
        
    }
    
}
