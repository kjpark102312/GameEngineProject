using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{

    public GameObject lineObj;
    public LineRenderer line;
    private LayerMask targetLayer;

    public float power = 1f;

    private bool isSpawnLaser;
    public IEnumerator LaserCo()
    {

        while(true)
        {
            SpawnLaser();

            yield return null;
        }
    }

    private void SpawnLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 100f);

        line.SetPosition(0, transform.position);
        if (hit)
        {
            line.SetPosition(1, hit.point);

            if (hit.transform.CompareTag("Obstacle") && isSpawnLaser == false)
            {
                for (int i = 2; i < 3; i++)
                {
                    Vector2 inNormal = Vector3.Normalize(transform.position - hit.transform.position);
                    Vector2 bounceVector = Vector3.Reflect(transform.right, hit.normal);

                    Debug.LogError(bounceVector);
                    Debug.DrawRay(hit.point, Vector3.Reflect(transform.right, hit.normal), Color.red);

                    RaycastHit2D hitObstacle = Physics2D.Raycast(hit.point, bounceVector, 100f);

                    line.positionCount++;

                    line.SetPosition(2, hit.transform.position + Vector3.Reflect(transform.right, hit.normal) * 10f);

                    Debug.Log(line.positionCount);
                }

                isSpawnLaser = true;

            }
            else if(hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyHp>().GetDamage(power);
                line.SetPosition(1, hit.point);
            }
        }


        

    }

    void Update()
    {
        SpawnLaser();
    }
}
