using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static string[] scenes = { "Playground", "Fogna", "Boschetto", "Città" };


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


}
