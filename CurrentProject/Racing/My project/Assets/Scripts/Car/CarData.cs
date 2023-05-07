using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Combined settings for car and gun controllers
public struct CarData
{
    public CarControllerSettings CarControllerSettings;
    public GunControllerSettings GunControllerSettings;

    public CarData(CarControllerSettings carControllerSettings, GunControllerSettings gunControllerSettings)
    {
        CarControllerSettings = carControllerSettings;
        GunControllerSettings = gunControllerSettings;
    }
    
}
