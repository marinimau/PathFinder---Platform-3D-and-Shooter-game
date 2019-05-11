using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SniperData
{
    public bool isDead;
    public float life;


    public SniperData(Sniper sniper)
    {
        /*isDead*/
        isDead = sniper.isDead;

        /*life*/
        life = sniper.life;

    }
}