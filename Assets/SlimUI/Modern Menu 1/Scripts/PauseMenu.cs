using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{


    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] public GameObject cameraObj;
    [SerializeField] private bool isPaused;
    Animator CameraObject;
    public GameObject riprendiBtn;
    public GameObject salvaBtn;
    public GameObject menuPrincipaleBtn;
    public GameObject areYouSure;

    void Start()
    {
        CameraObject = transform.GetComponent<Animator>();
        cameraObj.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            areYouSure.gameObject.SetActive(false);
        }
        if (isPaused)
        {
            ActivateMenu();

        }
        else
        {
            DeactivateMenu();
            cameraObj.SetActive(false);
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0; //freeze del tempo all'attivazione del menu
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        cameraObj.SetActive(true);

    }
    public void DeactivateMenu()
    {
        Time.timeScale = 1; //tempo continua a scorrere
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

   
    public void PlayCampaign()
    {
        areYouSure.gameObject.SetActive(false);
        riprendiBtn.gameObject.SetActive(true);
        salvaBtn.gameObject.SetActive(true);
        menuPrincipaleBtn.gameObject.SetActive(true);
    }
    public void DisablePlayCampaign()
    {
        riprendiBtn.gameObject.SetActive(true);
        salvaBtn.gameObject.SetActive(true);
        menuPrincipaleBtn.gameObject.SetActive(true);
    }
    public void Position1()
    {
        CameraObject.SetFloat("Animate", 0);
    }
    public void Position2()
    {
        CameraObject.SetFloat("Animate", 1);
    }

    // Are You Sure - Quit Panel Pop Up
    public void AreYouSure()
    {
        areYouSure.gameObject.SetActive(true);
        DisablePlayCampaign();
    }

    public void No()
    {
        areYouSure.gameObject.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }
}
