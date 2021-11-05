using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{

    public GameObject lineObj;
    public LineRenderer line;

    public LaserSpawner laserSpawner;

    public float power = 3f;

    private RaycastHit2D hit ;
    private RaycastHit2D[] hits;
    private Vector3 laserDir;

    private bool isBlock = false;
    private bool isHitObstacle = false;

    public int obstacleCount;
    public int rayCount;

    private void Awake() 
    {
        laserSpawner = GameObject.Find("LaserSpawner").GetComponent<LaserSpawner>();
    }

    private void Start() 
    {
        laserDir = laserSpawner.laserDir;
        rayCount = line.positionCount - 1;
    }

    void Update()
    {
        CastRay(transform.position, laserDir);
        EnemyHit(transform.position, laserDir);
    }

    void CastRay(Vector2 position, Vector2 dir)
    {
        line.SetPosition(0, transform.position);
        for (int i = 0; i < rayCount; i++ )
        {
            RaycastHit2D hit = Physics2D.Raycast(position, dir, 10f , 1 << 6);
            Debug.DrawRay(position, dir, Color.green);
            if (hit)
            {
                if (hit.transform.CompareTag("Obstacle"))
                {
                    position = hit.point;
                    dir = Vector3.Reflect(dir, hit.normal);
                    line.SetPosition(i + 1, hit.point);
                }
            }
            else
            {
                line.SetPosition(i+1, position + dir * 10f);
            }
        }
    }
    void EnemyHit(Vector2 position, Vector2 dir)
    {
        for (int i = 0; i < rayCount; i++)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, dir, 10f, 1 << 6 | 1 << 7);
            
            for (int j = 0; j < hits.Length; j++)
            {
                RaycastHit2D hit = hits[j];
                if (hit)
                {
                    if (hit.transform.CompareTag("Obstacle"))
                    {
                        position = hit.point;
                        dir = Vector3.Reflect(dir, hit.normal);
                        
                    }
                    else if (hit.transform.CompareTag("Enemy"))
                    {
                        hit.transform.GetComponent<EnemyHp>().GetDamage(power * Time.deltaTime);
                    }
                }
            }
        }
    }
}
