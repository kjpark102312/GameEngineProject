using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarSetter : MonoBehaviour
{
    public Vector3 distance = Vector2.up * 80f;
    public Transform targetTr;
    public RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTr = target;

        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(targetTr == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTr.position);

        rectTransform.position = screenPosition + distance;
    }
}
