using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
   
    public bool isBuildTower { set; get; }

    

    private void Awake()
    {
        isBuildTower = false;
    }

    private void Update()
    {
        ActiveTower();
    }

    void ActiveTower()
    {
        if (isBuildTower == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
