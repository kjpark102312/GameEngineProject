using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class EndPoint : MonoBehaviour
{

    public Image getDamagePanel;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            getDamagePanel.DOFade(0.4f , 0.1f).OnComplete(() =>
            {
                getDamagePanel.DOFade(0f, 0.3f);
            });

        }
    }
}
