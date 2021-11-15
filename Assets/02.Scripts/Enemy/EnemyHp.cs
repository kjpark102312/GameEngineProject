using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnemyHp : MonoBehaviour
{
    public float maxHP = 1000;
    public float currentHP = 1000;

    public GameObject floatingText;

    public float power;
    
    public void GetDamage()
    {
        currentHP -= power;

        Debug.Log("데미지 받는중");

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndPoint"))
        {
            GameManager.Instance.currentHp--;
            Destroy(gameObject);
        }
    }
}
