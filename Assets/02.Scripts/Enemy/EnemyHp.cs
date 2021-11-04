using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnemyHp : MonoBehaviour, IDamageable
{
    public float maxHP = 1000;
    public float currentHP = 1000;

    private float coolTime = 0.8f;
    private float baseTime = 0;

    public GameObject floatingText;

    public void GetDamage(float power)
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
