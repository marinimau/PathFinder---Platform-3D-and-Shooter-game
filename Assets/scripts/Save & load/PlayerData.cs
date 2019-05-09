using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int nColpi;
    public float[] position;
    public bool isDead;

    //immortalita
    public bool immortale;
    public float timer_immortale;

    //chiave
    public bool key;

    //invisibilità
    public bool invisibile;
    public float timer_invisibile;

    //contatti
    public bool player_contact;
    public bool boss_contact;
    public bool player_contact_deactivated;

    //game over
    public bool gameOver;


    public PlayerData(CharacterControllerScript characterControllerScript)
    {
        health = CharacterControllerScript.health;
        nColpi = GunScript.nColpi;

        //posizione
        position = new float[3];
        position[0] = characterControllerScript.transform.position.x;
        position[1] = characterControllerScript.transform.position.y;
        position[2] = characterControllerScript.transform.position.z;

        //immortalita
        immortale = CharacterControllerScript.immortality;
        timer_immortale = CharacterControllerScript.immortalityTimer;

        //chiave
        key = CharacterControllerScript.key;

        //invisibilità
        invisibile = CharacterControllerScript.invisible;
        timer_invisibile = CharacterControllerScript.invisibleTimer;

        //contatti
        boss_contact = CharacterControllerScript.boss_contact;
        player_contact = CharacterControllerScript.player_contact;
        player_contact_deactivated = CharacterControllerScript.player_contact_deactivated;

        //game over
        gameOver = CharacterControllerScript.gameOver;

    }
}
