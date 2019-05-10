using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class Save
{
    public static void playerSave(CharacterControllerScript characterControllerScipt)
    {
        /*--------------------------------------------------------------*
         * 
         * Player
         *
         *--------------------------------------------------------------*/
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(characterControllerScipt);

        formatter.Serialize(stream, playerData);
        stream.Close();

    }

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
        } else
        {
            Debug.Log("Errore caricamento, File non trovato");
            return null;
        }
    }

    public static void lightSave(lightPointCollider lpc)
    {
        /*--------------------------------------------------------------*
         * 
         * Lights
         *
         *--------------------------------------------------------------*/
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/light.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LightData lightData = new LightData(lpc);

        formatter.Serialize(stream, lightData);
        stream.Close();

    }

    public static LightData lightLoad()
    {
        string path = Application.persistentDataPath + "/light.fun";
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

}
