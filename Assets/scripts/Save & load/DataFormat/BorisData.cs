using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BorisData
{
    public bool isDead;
    public float life;
    public float[] position = new float[3];


    public BorisData(Boris boris)
    {
        /*isDead*/
        isDead = boris.isDead;

        /*life*/
        life = boris.life;

        /*position*/
        position[0] = boris.gameObject.transform.position.x;
        position[1] = boris.gameObject.transform.position.y;
        position[2] = boris.gameObject.transform.position.z;

    }
}
