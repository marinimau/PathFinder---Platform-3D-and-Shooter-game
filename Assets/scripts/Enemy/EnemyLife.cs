using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    public int life;
    public bool isDead;
    GameObject head;
    GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decrLife(int damage)
    {
        if (life - damage > 0)
        {
            life -= damage;
        }
        else
        {
            isDead = true;
        }

    }

    public void incLife(int cura)
    {
        if (life + cura <= 100)
        {
            life += cura;
        }
        else
        {
            life = 100;
        }
    }

}
