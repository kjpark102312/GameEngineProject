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
}
