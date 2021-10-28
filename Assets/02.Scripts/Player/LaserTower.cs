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
    private Vector2 laserDir;

    private int laserCount = 1;

    private bool laserHit = true;

    private bool isBlock = false;

    private bool isHitObstacle = false;
    public enum LaserHitState
    {
        hitObstacle,
        hitEnemy,
        hitAnything
    }

    public LaserHitState hitState = LaserHitState.hitAnything;

    public void CheckLaserState()
    {
        
        if (hit)
        {
            if (hit.transform.CompareTag("Obstacle")) ChangeLaserState(LaserHitState.hitObstacle);
            Debug.Log("�ɽ�ŸŬ �¾ƹ�����");
        }
        else
        {
            ChangeLaserState(LaserHitState.hitAnything);
            Debug.Log("�ƹ����� �ȸ¾ƹ�����");
        }
    }

    private void ChangeLaserState(LaserHitState hitState)
    {
        switch(hitState)
        {
            case LaserHitState.hitObstacle:
                SpawnLaser();
                break;
            case LaserHitState.hitAnything:
                break;
        }
    }

    private void Awake() 
    {
        laserSpawner = GameObject.Find("LaserSpawner").GetComponent<LaserSpawner>();
    }

    private void Start() 
    {
        laserSpawner.LaserDir(hit);
        laserDir = laserSpawner.laserDir;
        CheckLaserState();
    }

    void Update()
    {
        HitAnything();
        EnemyHit();
        ReflectLaser();
    }

    private void SpawnLaser()
    {
        laserCount = 1;
        Vector2 bounceVector = Vector3.Reflect(laserDir, hit.normal);
        Debug.DrawRay(hit.point, Vector3.Reflect(laserDir, hit.normal), Color.red);

        hitObstacle = Physics2D.Raycast(hit.point, bounceVector, 100f);

        laserCount++;
        line.SetPosition(1, hit.point);
        line.SetPosition(laserCount, hit.transform.position + Vector3.Reflect(laserDir, hit.normal) * 10f);

        Debug.Log(line.positionCount);
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
                        //line.SetPosition(0, transform.position);
                        //line.SetPosition(1, laserDir * 100);
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
                laserCount = 2;
                line.positionCount = 3;
                line.SetPosition(1, hit.point);
                line.SetPosition(2, hit.transform.position + Vector3.Reflect(laserDir, hit.normal) * 10f);

                Debug.DrawRay(hit.point, Vector3.Reflect(laserDir, hit.normal), Color.green);
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                HitEnemyAndObstacle();
            }
        }
        else if(!isHitObstacle)
        {
            line.SetPosition(1, laserDir * 100);
        }
    }

    private void HitEnemyAndObstacle()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(hit.point, Vector3.Reflect(laserDir, hit.normal), 100f, 1 << 6);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D bounceHit = hits[i];
            if (bounceHit)
            {
                if (bounceHit.transform.CompareTag("Obstacle"))
                {
                    isHitObstacle = true;
                }
                else if(!isHitObstacle)
                {
                }
            }
        }
    }
}
