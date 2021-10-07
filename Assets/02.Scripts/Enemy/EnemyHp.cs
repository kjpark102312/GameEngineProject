using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour, IDamageable
{

    public float hp = 5;
    
    public void GetDamage(float power)
    {
        hp -= power;
    }
}