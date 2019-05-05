using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualeravvicinataBoris : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    public Boris boris;

    void Start()
    {
        anim = transform.parent.GetComponentInChildren<Animator>();
        boris = GetComponentInParent<Boris>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !CharacterControllerScript.gameOver && !boris.isDead && !CharacterControllerScript.player_contact && !CharacterControllerScript.invisible)
        {
            CharacterControllerScript.player_contact = true;
            Show_stealth_status.icon = 2;
            Debug.Log(" player dentro il cono piccolo");
        }
    }

}
