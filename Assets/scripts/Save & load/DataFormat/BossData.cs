using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossData
{
    public bool isDead;
    public float life;
    public float[] position = new float[3];
    public int nColpi;


    public BossData(Boss boss)
    {
        /*isDead*/
        isDead = boss.isDead;

        /*life*/
        life = Boss.life;

        /*position*/
        position[0] = boss.gameObject.transform.position.x;
        position[1] = boss.gameObject.transform.position.y;
        position[2] = boss.gameObject.transform.position.z;

        /*nColpi*/
        nColpi = boss.nColpi;

    }
}
