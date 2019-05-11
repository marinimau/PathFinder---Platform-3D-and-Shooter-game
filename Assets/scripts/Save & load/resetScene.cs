using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetScene : MonoBehaviour
{
    public GameObject player;
    CharacterControllerScript ccs;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player&UI");
        ccs = player.GetComponentInChildren<CharacterControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Reset()
    {
        SceneManager.LoadScene("playground", LoadSceneMode.Single);
        player = GameObject.FindGameObjectWithTag("Player&UI");
        ccs = player.GetComponentInChildren<CharacterControllerScript>();
        ccs.LoadPlayer();
        Debug.Log("fine reset");
    }
}
