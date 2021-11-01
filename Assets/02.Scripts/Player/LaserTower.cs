using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{

    public GameObject lineObj;
    public LineRenderer line;
    private LayerMask targetLayer;

    public LaserSpawner laserSpawner;

    public float power = 3f;

    private bool isSpawnLaser;
    private RaycastHit2D hit ;
    private RaycastHit2D hitObstacle;
    private RaycastHit2D[] hits;
    private Vector3 laserDir;

    private int laserCount = 1;

    private bool isBlock = false;
    private bool isHitObstacle = false;

    private float coolTime = 0.5f;
    private float realTime = 0f;

    public int rayCount;

    private void Awake() 
    {
        laserSpawner = GameObject.Find("LaserSpawner").GetComponent<LaserSpawner>();
    }

    private void Start() 
    {
        laserDir = laserSpawner.laserDir;
        
    }

    void Update()
    {
        //HitAnything();
        //EnemyHit();
        //ReflectLaser();
        //Debug.DrawRay(transform.position, laserDir * 10, Color.green);
        CastRay(transform.position, laserDir);
        Debug.DrawRay(transform.position, laserDir);
    }

    void CastRay(Vector2 position, Vector2 dir)
    {
        line.SetPosition(0, transform.position);
        for (int i = 0; i < rayCount; i++ )
        {
            RaycastHit2D hit = Physics2D.Raycast(position, dir, 10f , 1<<6);
            if(hit)
            {
                if (hit.transform.CompareTag("Obstacle"))
                {
                    position = hit.point;
                    dir = Vector3.Reflect(dir, hit.normal);
                    Debug.DrawRay(position, dir, Color.green);
                    Debug.DrawRay(hit.point, Vector2.Reflect(laserDir, hit.normal), Color.red);
                    line.SetPosition(i + 1, hit.point);
                }
            }
            else
            {
                line.SetPosition(i+1, transform.position + laserDir * 10f);
                //Debug.DrawRay(position, dir * 10, Color.red);
                break;
            }
        }
    }
    
    private Vector2 CheckBounceLaser(Vector2 inDirection)
    {
        Vector2 inNormal = Vector3.Reflect(inDirection, hit.normal);

        return inNormal;
    }

    

    private void EnemyHit()
    {
        hits = Physics2D.RaycastAll(gameObject.transform.position, laserDir, 100f, 1 << 6);

        if(!isBlock)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit)
                {
                    
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        hit.transform.GetComponent<EnemyHp>().GetDamage(power * Time.deltaTime);
                    }
                }
            }
        }
    }

    private void ReflectLaser()
    {
        if(hit)
        {
            if (hit.transform.CompareTag("Obstacle"))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(hit.point, Vector3.Reflect(laserDir, hit.normal), 100f, 1 << 6);

                for (int j = 0; j < hits.Length; j++)
                {
                    RaycastHit2D bounceHit = hits[j];
                    if (bounceHit)
                    {
                        if (bounceHit.transform.CompareTag("Enemy"))
                        {
                            bounceHit.transform.GetComponent<EnemyHp>().GetDamage(power * Time.deltaTime);
                        }
                    }
                }
            }
        }
    }

    private void HitAnything()
    {
        hit = Physics2D.Raycast(transform.position, laserDir, 100f, 1 << 6);

        line.SetPosition(0, transform.position);

        if(hit)
        {
            if(hit.transform.CompareTag("Obstacle"))
            {
                Debug.LogError("??");

                laserCount = 2;
                line.positionCount = 3;
                line.SetPosition(1, hit.point);
                line.SetPosition(2, hit.point + Vector2.Reflect(laserDir, hit.normal) * 10f);

                Debug.DrawRay(hit.point, Vector2.Reflect(laserDir, hit.normal), Color.green);

                isHitObstacle = true;
            }
            else if (hit.transform.CompareTag("Enemy") && !isHitObstacle)
            {
                HitEnemyAndObstacle();
            }
        }
        else if(!isHitObstacle)
        {
            line.SetPosition(1, transform.position + laserDir * 10f);
        }
    }

    private void HitEnemyAndObstacle()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, laserDir, 100f, 1 << 6);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D bounceHit = hits[i];
            if (bounceHit)
            {
                if (bounceHit.transform.CompareTag("Obstacle"))
                {
                    Debug.LogError("??");
                    laserCount = 2;
                    line.positionCount = 3;
                    line.SetPosition(1, bounceHit.point);
                    line.SetPosition(2, bounceHit.point + Vector2.Reflect(laserDir, hit.normal) * 10f);
                    isHitObstacle = true;
                }
                else if(!isHitObstacle)
                {
                    Debug.Log("Àû¸ÂÀ½");
                    line.SetPosition(1, transform.position + laserDir * 100);
                }
            }
        }
    }
}
