using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    private int sceneId;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
          
    }

    // Update is called once per frame
    public void Save()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fStream = File.Create(Application.persistentDataPath + "saveFile.octo");

        SaveManager saver = new SaveManager();
        sceneName = SceneManager.GetActiveScene().name;
        sceneId = SceneManager.GetActiveScene().buildIndex;

        saver.levelSaved = sceneName;
        //saver.levelSaved = sceneId;
        //All other to insert on binary file



        binary.Serialize(fStream, saver);
        fStream.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "saveFile.octo"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fStream = File.Open(Application.persistentDataPath + "saveFile.octo", FileMode.Open);
            SaveManager saver = (SaveManager)(binary.Deserialize(fStream));
            fStream.Close();

            SceneManager.LoadScene(saver.levelSaved);

           //this.gameObject.GetComponent<ChangeSceneAsync>().ChangeScene(saver.levelSaved);
        }
    }
}

[Serializable]
class SaveManager
{
    public string levelSaved;
    //public int levelSaved;
    //Do stuff
}
