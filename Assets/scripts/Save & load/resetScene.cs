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


}
