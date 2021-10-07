using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{

    public bool isBuildObstacle { set; get; }

    private void Awake()
    {
        isBuildObstacle = false;   
    }

    private void Update()
    {
        ActivePoint();
    }

    void ActivePoint()
    {
        if(isBuildObstacle == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
