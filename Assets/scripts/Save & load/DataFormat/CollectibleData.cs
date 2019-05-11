using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectibleData
{
    public bool active;


    public CollectibleData(collectible collectible)
    {
        active = collectible.active;

    }
}
