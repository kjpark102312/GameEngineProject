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
        //FloatingText(power);

        Debug.Log("데미지 받는중");

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void FloatingText(float power)
    {
        baseTime += Time.deltaTime;

        if (coolTime <= baseTime)
        {
            GameObject Canvas = Instantiate(floatingText);
            GameObject text = Canvas.GetComponentInChildren<GameObject>();
            text.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.2f, 0));
            text.transform.position = transform.position + new Vector3(0f, 3f);
            text.GetComponent<FloatingText>().power = power;
            baseTime = 0;
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
