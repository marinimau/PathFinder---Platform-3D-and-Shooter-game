using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetScene : MonoBehaviour
{
    //player
    public GameObject player;
    CharacterControllerScript ccs;
    //luci
    public GameObject[] light;
    lightPointCollider[] lpc;

    bool unloaded;
    // Start is called before the first frame update
    void Start()
    {
        this.unloaded = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Reset()
    {
        SceneManager.LoadScene("playground", LoadSceneMode.Single);
        this.unloaded = true;
    }


    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (!this.unloaded)
        {
            /*player*/
            player = GameObject.FindGameObjectWithTag("Player&UI");
            ccs = player.GetComponentInChildren<CharacterControllerScript>();
            ccs.LoadPlayer();

            /*light*/
            light = GameObject.FindGameObjectsWithTag("LightPoint");
            for (int i = 0; i < light.Length; i++)
            {
                light[i].GetComponent<lightPointCollider>().LoadLight();
                Debug.Log("luce [" + i + "] stato: " + light[i].GetComponent<lightPointCollider>().accesa);
            }
            Debug.Log("fine reset");
            this.unloaded = false;
        }
    }
}
