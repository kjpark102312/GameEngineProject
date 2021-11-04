using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickWaypoints : MonoBehaviour
{
    private int maxChance = 7;
    private int currentChance = 1;

    public GameObject waypoint;

    private SpriteRenderer sr;
    public Sprite[] sprites;

    public List<GameObject> wayPoints = new List<GameObject>();

    private void Update()
    {
        Click();
    }

    private void Click()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, Mathf.Infinity);

            if (hit.collider == null) return;
            if (hit.transform.CompareTag("Tile"))
            {
                if(currentChance != maxChance)
                {
                    GameObject Clone = Instantiate(waypoint, hit.transform.position, Quaternion.identity);
                    sr = Clone.GetComponent<SpriteRenderer>();
                    sr.sprite = sprites[currentChance - 1];
                    wayPoints.Add(Clone);
                    currentChance++;
                }
            }
        }
    }
}
