using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolData
{
    public bool isDead;
    public float life;
    public float[] position=new float[3];


    public PatrolData(Patrol patrol)
    {
        /*isDead*/
        isDead = patrol.isDead;

        /*life*/
        life = patrol.life;

        /*position*/
        position[0] = patrol.gameObject.transform.position.x;
        position[1] = patrol.gameObject.transform.position.y;
        position[2] = patrol.gameObject.transform.position.z;

    }
}
