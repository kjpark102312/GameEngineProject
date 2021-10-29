using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI floatingText;
    private float moveSpeed = 2f;
    private float alphaSpeed = 2f;
    private float destroyTime = 2f;
    private Color alpha;
    public float power;
    private void Start()
    {
        floatingText = GetComponent<TextMeshProUGUI>();
        floatingText.text = power.ToString();
        Debug.Log(alpha);

        Invoke("DestroyObject", destroyTime);
    }
    private void Update()
    {
        //Floating();
    }

    private void Floating()
    {
        Debug.Log("플로팅중");
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        floatingText.color = alpha;
    }

    private void DestroyObject()
    {
        Debug.Log("Asdad");
        Destroy(gameObject.transform.parent.gameObject);
    }

    
    
}
