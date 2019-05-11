using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightData
{
    public bool accesa;


    public LightData(lightPointCollider light)
    {
        accesa = light.accesa;

    }
}
