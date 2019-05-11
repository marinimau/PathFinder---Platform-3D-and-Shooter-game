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
        ccs = player.GetComponent<CharacterControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Reset()
    {
        SceneManager.LoadScene("playground", LoadSceneMode.Single);
        ccs.LoadPlayer();
    }
}
