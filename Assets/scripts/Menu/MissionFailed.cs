using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionFailed : MonoBehaviour
{


    Animator CameraObject;

    [Header("Loaded Scene")]
    [Tooltip("The name of the scene in the build settings that will load")]
    public string sceneName = "MenuPrincipale_scena";

    [Header("PLAY Sub-Buttons")]

    public GameObject riprendiUltimo;
    [Tooltip("New Game Button GameObject Pop Up")]
    public GameObject menuBtn;
    [Tooltip("The UI Pop-Up when 'EXIT' is clicked")]
    public GameObject PanelareYouSure;

    void Start()
    {
        CameraObject = transform.GetComponent<Animator>();
    }

    public void Play()
    {
        PanelareYouSure.gameObject.SetActive(false);
        riprendiUltimo.gameObject.SetActive(true);
        menuBtn.gameObject.SetActive(true);
    }

    public void DisablePlay()
    {
        riprendiUltimo.gameObject.SetActive(false);
        menuBtn.gameObject.SetActive(false);
    }

    public void Position2()
    {
        DisablePlay();
        CameraObject.SetFloat("Animate", 1);
    }

    public void Position1()
    {
        CameraObject.SetFloat("Animate", 0);
    }

    // Are You Sure - Quit Panel Pop Up
    public void AreYouSure()
    {
        PanelareYouSure.gameObject.SetActive(true);
        riprendiUltimo.gameObject.SetActive(true);
        menuBtn.gameObject.SetActive(true);
        //DisablePlay();
    }

    public void No()
    {
        PanelareYouSure.gameObject.SetActive(false);
    }

    public void Yes()
    {
           if (sceneName != "")
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        
    }

}