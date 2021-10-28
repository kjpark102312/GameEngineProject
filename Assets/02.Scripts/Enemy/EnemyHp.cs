using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnemyHp : MonoBehaviour, IDamageable
{

    public float hp = 5;

    private float coolTime = 0.8f;
    private float baseTime = 0;

    public GameObject floatingText;

    private SpriteRenderer sr;

    public void GetDamage(float power)
    {
        hp -= power;
        FloatingText(power);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void FloatingText(float power)
    {
        baseTime += Time.deltaTime;

        if (coolTime <= baseTime)
        {
            Debug.Log("데미지 텍스투");
            GameObject text = Instantiate(floatingText);
            text.transform.position = transform.position + new Vector3(0f, 3f);
            text.GetComponent<FloatingText>().power += power;
            text.GetComponent<FloatingText>().power = 0;
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
