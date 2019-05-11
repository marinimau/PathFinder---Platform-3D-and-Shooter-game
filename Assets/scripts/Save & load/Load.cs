using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class Load
{
    /*--------------------------------------------------------------*
     * 
     * Player
     *
     *--------------------------------------------------------------*/
    public static PlayerData playerLoad()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Errore caricamento, File non trovato");
            return null;
        }
    }

    /*--------------------------------------------------------------*
     * 
     * Lights
     *
     *--------------------------------------------------------------*/
    public static LightData lightLoad(int i)
    {
        string path = Application.persistentDataPath + "/light"+i+".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LightData data = formatter.Deserialize(stream) as LightData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Errore caricamento, File non trovato");
            return null;
        }
    }

    /*--------------------------------------------------------------*
     * 
     * Collectibles
     *
     *--------------------------------------------------------------*/
    public static CollectibleData collectibleLoad(int i)
    {
        string path = Application.persistentDataPath + "/collectible" + i + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CollectibleData data = formatter.Deserialize(stream) as CollectibleData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Errore caricamento, File non trovato");
            return null;
        }
    }

    /*--------------------------------------------------------------*
     * 
     * Sniper
     *
     *--------------------------------------------------------------*/
    public static SniperData sniperLoad(int i)
    {
        string path = Application.persistentDataPath + "/sniper" + i + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SniperData data = formatter.Deserialize(stream) as SniperData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Errore caricamento, File non trovato");
            return null;
        }
    }




    /*--------------------------------------------------------------*
     *
     *
     * 
     *
     *
     * 
     * DO_LOAD_ALL
     *
     * 
     * 
     * 
     * 
     * 
     *--------------------------------------------------------------*/

    public static void doLoadAll()
    {

        /*--------------------------------------------------------------*
         * 
         * Player
         *
         *--------------------------------------------------------------*/
        /*recupero i campi da salvare*/
        CharacterControllerScript cs = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        PlayerData playerData = playerLoad();
        /*li salvo*/
        //health
        CharacterControllerScript.health = playerData.health;
        //nColpi
        GunScript.nColpi = playerData.nColpi;

        //posizione
        Vector3 savedPosition = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);

        cs.gameObject.transform.position = savedPosition;

        //immortalita
        CharacterControllerScript.immortality = playerData.immortale;
        CharacterControllerScript.immortalityTimer = playerData.timer_immortale;

        //chiave
        CharacterControllerScript.key = playerData.key;

        //invisibilità
        CharacterControllerScript.invisible = playerData.invisibile;
        CharacterControllerScript.invisibleTimer = playerData.timer_invisibile;

        //contatti
        CharacterControllerScript.boss_contact = playerData.boss_contact;
        //se ero in una situazione di contatto ma quando carico non lo sono più
        if (CharacterControllerScript.player_contact && !playerData.player_contact)
        {
            CharacterControllerScript.player_contact_deactivated = true;
        }
        else
        {
            CharacterControllerScript.player_contact_deactivated = playerData.player_contact_deactivated;
        }
        CharacterControllerScript.player_contact = playerData.player_contact;


        //game over
        CharacterControllerScript.gameOver = playerData.gameOver;
        //isDead
        cs.animator.Rebind();
        CharacterControllerScript.isDead = playerData.isDead;


        /*--------------------------------------------------------------*
         * 
         * Luci
         *
         *--------------------------------------------------------------*/
        /*recupero i campi da salvare*/
        GameObject[] light = GameObject.FindGameObjectsWithTag("LightPoint");
        LightData lightData;

        for (int i = 0; i < light.Length; i++)
        {
            lightData = lightLoad(i);
            light[i].GetComponent<lightPointCollider>().accesa = lightData.accesa;
        }

        /*--------------------------------------------------------------*
         * 
         * collectible
         *
         *--------------------------------------------------------------*/
        /*recupero i campi da salvare*/
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        CollectibleData collectibleData;

        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibleData = collectibleLoad(i);
            collectibles[i].GetComponent<collectible>().active = collectibleData.active;
        }

        /*--------------------------------------------------------------*
         * 
         * sniper
         *
         *--------------------------------------------------------------*/
        /*recupero i campi da salvare*/
        GameObject[] snipers = GameObject.FindGameObjectsWithTag("Sniper");
        SniperData sniperData;

        for (int i = 0; i < snipers.Length; i++)
        {
            sniperData = sniperLoad(i);
            snipers[i].GetComponent<Sniper>().isDead = sniperData.isDead;
            snipers[i].GetComponent<Sniper>().life = sniperData.life;
            snipers[i].GetComponent<Sniper>().animEnemy.Rebind();
        }
    }

}
