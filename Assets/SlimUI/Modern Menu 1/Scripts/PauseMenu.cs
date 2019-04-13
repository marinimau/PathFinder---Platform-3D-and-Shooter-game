using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{


    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0; //freeze del tempo all'attivazione del menu
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }
    public void DeactivateMenu()
    {
        Time.timeScale = 1; //tempo continua a scorrere
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
