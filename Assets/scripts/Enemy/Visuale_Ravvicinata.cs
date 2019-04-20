using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visuale_Ravvicinata : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    public Patrol enemy;

    void Start()
    {
        anim = transform.parent.GetComponentInChildren<Animator>();
        enemy = GetComponentInParent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isDead)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !EnemySight.player_contact && !CharacterControllerScript.invisible)
        {
            EnemySight.player_contact = true;
            Show_stealth_status.icon = 2;
            //è per forza nella stessa luce che interseca il cono perchè il controllo lo facciamo quando il player è già nel cono
            Debug.Log(" player dentro il cono piccolo");
        }
        if (anim.GetBool("isHeadHit"))
        {
            this.enabled = false;

        }
    }

}
