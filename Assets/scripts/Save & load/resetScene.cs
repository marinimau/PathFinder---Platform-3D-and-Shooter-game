using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "player.fun"))
        {
            Save.doSaveAll();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Salva()
    {
        Save.doSaveAll();
    }

    public void Carica()
    {
        Load.doLoadAll();
        PauseMenu.chiudi = true;

    }

    public void Ricomincia()
    {
        Show_stealth_status.icon = 0;
        PauseMenu.chiudi = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        CharacterControllerScript cs = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        cs.animator.Rebind();
        //Load.doLoadAll();
        PauseMenu.chiudi = true;
        PauseMenu.isPaused = false;
    }

}
