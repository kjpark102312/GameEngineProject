using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{

    public GameObject lineObj;
    public LineRenderer line;
    private LineRenderer splitLine;

    public LaserSpawner laserSpawner;

    public float power = 3f;

    private Vector3 laserDir;

    public int obstacleCount;
    public int rayCount;

    public List<Vector2> colPoints = new List<Vector2>();

    private EnemyHp enemy;
    private EdgeCollider2D col;

    bool istest = true;
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
        CastRay(transform.position, laserDir, line);
        EnemyHit(transform.position, laserDir);
    }

    private float CheckDis(Vector2 position, Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, dir, Mathf.Infinity, 1 << 6);

        if(hit)
        {
            return Mathf.Round(Vector2.Distance(position, hit.point));
        }
        else
        {
            return 10f;
        }
    }

    

    void CastRay(Vector2 position, Vector2 dir, LineRenderer line)
    {
        line.SetPosition(0, transform.position);
        for (int i = 0; i < rayCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, dir, CheckDis(position, dir), 1 << 6);
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

                    InstantiateLaser(position, dir, i);

                    dir = Vector3.Reflect(dir, hit.normal);
                    line.SetPosition(i + 1, hit.point);
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
            Debug.DrawRay(position, dir, Color.green);
            for (int j = 0; j < hits.Length; j++)
            {
                RaycastHit2D hit = hits[j];
                if (hit)
                {
                    if (hit.transform.CompareTag("Obstacle") || hit.transform.CompareTag("Splitter"))
                    {
                        position = hit.point;
                        dir = Vector3.Reflect(dir, hit.normal);
                    }
                    else if (hit.transform.CompareTag("Enemy"))
                    {
                        enemy = hit.transform.GetComponent<EnemyHp>();

                        enemy.power = power * Time.deltaTime;
                        enemy.GetDamage();
                    }
                }
            }
        }
    }

    private void InstantiateLaser(Vector2 position, Vector2 dir, int i)
    {
        if(this.istest)
        {
            GameObject laser = Instantiate(lineObj, position, Quaternion.identity);
            laser.transform.parent = transform.parent.parent;

            splitLine = laser.GetComponent<LineRenderer>();
        }
        
        Vector2 originDir = dir;

        CastRay(position, originDir, splitLine);
        EnemyHit(position, originDir);

        this.istest = false;
    }
}
