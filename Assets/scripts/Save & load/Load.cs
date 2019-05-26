using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


public static class Load
{
    /*--------------------------------------------------------------*
     * 
     * Player
     *
     *--------------------------------------------------------------*/
    public static PlayerData playerLoad()
    {
        string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "player.fun";
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
        string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "light" +i+".fun";
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
        string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "collectible" + i + ".fun";
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
        string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "sniper" + i + ".fun";
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
     * Boris
     *
     *--------------------------------------------------------------*/
    public static BorisData borisLoad(int i)
    {
        string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "boris" + i + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BorisData data = formatter.Deserialize(stream) as BorisData;
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
     * Patrol
     *
     *--------------------------------------------------------------*/
    public static PatrolData patrolLoad(int i)
    {
        string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "patrol" + i + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PatrolData data = formatter.Deserialize(stream) as PatrolData;
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

        //special bullet
        CharacterControllerScript.specialBullet = playerData.special_bullet;
        CharacterControllerScript.specialBulletTimer = playerData.timer_special_bullet;

        //chiave
        CharacterControllerScript.key = playerData.key;

        //invisibilità
        CharacterControllerScript.invisible = playerData.invisibile;
        CharacterControllerScript.invisibleTimer = playerData.timer_invisibile;

        //contatti
        CharacterControllerScript.boss_contact = playerData.boss_contact;
        //se ero in una situazione di contatto ma quando carico non lo sono più

        //if (CharacterControllerScript.player_contact && !playerData.player_contact)
        //{
        //    CharacterControllerScript.player_contact_deactivated = true;
        //    CharacterControllerScript.player_contact = false;
        //}
        //else
        //{
        //    CharacterControllerScript.player_contact_deactivated = playerData.player_contact_deactivated;
        //}



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

        /*--------------------------------------------------------------*
         * 
         * boris
         *
         *--------------------------------------------------------------*/
        /*recupero i campi da salvare*/
        GameObject[] boris = GameObject.FindGameObjectsWithTag("Boris");
        BorisData borisData;

        for (int i = 0; i < boris.Length; i++)
        {
            borisData = borisLoad(i);
            boris[i].GetComponentInChildren<Boris>().isDead = borisData.isDead;
            boris[i].GetComponentInChildren<Boris>().life = borisData.life;
            boris[i].GetComponentInChildren<Boris>().transform.position=new Vector3(borisData.position[0], borisData.position[1], borisData.position[2]);
            boris[i].GetComponentInChildren<Boris>().animBoris.Rebind();
        }

        /*--------------------------------------------------------------*
        * 
        * patrols
        *
        *--------------------------------------------------------------*/
        /*recupero i campi da salvare*/
        GameObject[] patrols = GameObject.FindGameObjectsWithTag("Patrol");
        PatrolData patrolData;

        for (int i = 0; i < patrols.Length; i++)
        {
            patrolData = patrolLoad(i);
            patrols[i].GetComponentInChildren<Patrol>().isDead = patrolData.isDead;
            patrols[i].GetComponentInChildren<Patrol>().life = patrolData.life;
            patrols[i].GetComponentInChildren<Patrol>().transform.position = new Vector3(patrolData.position[0], patrolData.position[1], patrolData.position[2]);
            patrols[i].GetComponentInChildren<Patrol>().animEnemy.Rebind();
            if (!patrolData.isDead)
            {
                patrols[i].GetComponentInChildren<Patrol>().navMesh.isStopped = false;
            }
            
        }


        if (playerData.player_contact)
            Show_stealth_status.icon = 2;
        else
            Show_stealth_status.icon = 0;
        //CharacterControllerScript.player_contact = false;
        //CharacterControllerScript.player_contact_deactivated = true;

        //CharacterControllerScript.player_contact = playerData.player_contact;


        ShowMessage.id = 13;
    }

}
