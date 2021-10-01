using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isBuildObstacle { set; get; }

    private void Awake()
    {
        isBuildObstacle = false;
    }
}
