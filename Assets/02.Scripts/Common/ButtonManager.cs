using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private LaserSpawner laserSpawner;

    public void OnClick()
    {
        laserSpawner.BuyLaserTower(this.gameObject);
    }
}
