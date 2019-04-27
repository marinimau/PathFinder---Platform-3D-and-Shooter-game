﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualeRavvicinataSniper : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    public Sniper sniper;

    void Start()
    {
        anim = transform.parent.GetComponentInChildren<Animator>();
        sniper = GetComponentInParent<Sniper>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !CharacterControllerScript.gameOver && !sniper.isDead && !CharacterControllerScript.player_contact && !CharacterControllerScript.invisible)
        {
            CharacterControllerScript.player_contact = true;
            Show_stealth_status.icon = 2;
            Debug.Log(" player dentro il cono piccolo");
        }
    }

}
