using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
   
    public static string[] scenes = { "Playground", "Fogna", "Boschetto", "Città" };
    public static Boolean[] sceneLoaded = { false, false, false, false};


    public SceneLoader()
    {

    }

    public static void loadNextLevel(){
        for (int i = 0; i < scenes.Length; i++){
            if(SceneManager.GetActiveScene().name==scenes[i]){
                SceneManager.LoadScene(scenes[i+1], LoadSceneMode.Single);
                return;
            }
        }
    }


    public static void loadPrevLevel()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == scenes[i])
            {
                SceneManager.LoadScene(scenes[i - 1], LoadSceneMode.Single);
                return;
            }
        }
    }

    public static void loadLevelById(int id)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if (i==id)
            {
                SceneManager.LoadScene(scenes[i], LoadSceneMode.Single);
                return;
            }
        }
    }

    public static void loadMostAdvancedProgress()
    {
        string path = "";
        for (int i = scenes.Length - 1; i >= 0; i--)
        {
            path = Application.persistentDataPath + "/" + scenes[i] + "player.fun";
            if (File.Exists(path))
            {
                SceneManager.LoadScene(scenes[i], LoadSceneMode.Single);
                if (SceneManager.GetActiveScene().isLoaded)
                {
                    sceneLoaded[i] = true;

                }
                return;
            }
            Debug.Log(path);
        }
        //se non ho salvataggio carico la prima scena e basta
        SceneManager.LoadScene(scenes[1], LoadSceneMode.Single);
        return;
    }


}
