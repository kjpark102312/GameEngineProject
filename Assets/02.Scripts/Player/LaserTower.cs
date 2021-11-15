using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{

    public GameObject lineObj;
    public LineRenderer line;

    public LaserSpawner laserSpawner;

    public float power = 3f;

    private RaycastHit2D hit;
    private RaycastHit2D[] hits;
    private Vector3 laserDir;

    private bool isBlock = false;
    private bool isHitObstacle = false;

    public int obstacleCount;
    public int rayCount;

    Vector2 direction;

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

    private float CheckDis(Vector2 position, Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, dir, Mathf.Infinity, 1 << 6);

        if(hit)
        {
            Debug.Log(Vector2.Distance(position, hit.point));
            return Mathf.Round(Vector2.Distance(position, hit.point));
        }
        else
        {
            return 10f;
        }
    }

    

    void CastRay(Vector2 position, Vector2 dir)
    {
        line.SetPosition(0, transform.position);
        for (int i = 0; i < rayCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, dir, CheckDis(position, dir), 1 << 6);
            Debug.DrawRay(position, dir, Color.green);
            if (hit)
            {
                if (hit.transform.CompareTag("Obstacle"))
                {
                    position = hit.point;
                    dir = Vector3.Reflect(dir, hit.normal).normalized;

                    Debug.Log(dir.x);
                    if (dir.x == 1.0f)
                    {
                        position.x = position.x + 0.1f;
                    }
                    else if (dir.x == -1.0f)
                    {
                        position.x = position.x - 0.1f;
                    }
                    else if (dir.y == 1.0f)
                    {
                        position.y = position.y + 0.1f;
                    }
                    else if (dir.y == -1.0f)
                    { 
                        position.y = position.y - 0.1f;
                    }
                    line.SetPosition(i + 1, hit.point);
                }
                else if (hit.transform.CompareTag("Splitter"))
                {
                    position = hit.point;

                    Vector2 originDir = dir;
                    dir = Vector3.Reflect(dir, hit.normal);
                    line.SetPosition(i + 1, hit.point);

                    GameObject laser = Instantiate(lineObj, position, Quaternion.identity);

                    LineRenderer splitLine = laser.GetComponent<LineRenderer>();
                    splitLine.SetPosition(0, position);
                    splitLine.SetPosition(i + 1, position + originDir * 10);
                }
            }
            else
            {
                line.SetPosition(i + 1, position + dir * 10f);
                Debug.LogWarning("여기여기요");
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
