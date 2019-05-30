using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour {

    Animator CameraObject;

    [Header("Loaded Scene")]
    [Tooltip("The name of the scene in the build settings that will load")]
    public string sceneName = "Fogna";

    [Header("PLAY Sub-Buttons")]
    [Tooltip("Continue Button GameObject Pop Up")]
    public GameObject continueBtn;
    [Tooltip("New Game Button GameObject Pop Up")]
    public GameObject newGameBtn;
    [Tooltip("Load Game Button GameObject Pop Up")]
    public GameObject PanelareYouSure;

    [Tooltip("Select level Button GameObject Pop Up")]
    public GameObject selectLevelBtn;
    [Tooltip("level1 Button GameObject Pop Up")]
    public GameObject level1Btn;
    [Tooltip("level2 Button GameObject Pop Up")]
    public GameObject level2Btn;
    [Tooltip("level3 Button GameObject Pop Up")]
    public GameObject level3Btn;

    void Start(){
		CameraObject = transform.GetComponent<Animator>();
	}

	public void  PlayCampaign (){
		PanelareYouSure.gameObject.SetActive(false);
        DisavleSelectLevel();
		continueBtn.gameObject.SetActive(true);
		newGameBtn.gameObject.SetActive(true);
	}

	public void NewGame(){
		if(sceneName != ""){
            SceneLoader.loadLevelById(1);
		}
	}
    

	public void  DisablePlayCampaign (){
		continueBtn.gameObject.SetActive(false);
		newGameBtn.gameObject.SetActive(false);
	}

    public void ContinueCampaign()
    {
        SceneLoader.loadMostAdvancedProgress();
    }


    public void SelectLevel()
    {
        PanelareYouSure.gameObject.SetActive(false);
        DisablePlayCampaign();
        level1Btn.gameObject.SetActive(true);
        level2Btn.gameObject.SetActive(true);
        level3Btn.gameObject.SetActive(true);
    }


    public void DisavleSelectLevel()
    {
        level1Btn.gameObject.SetActive(false);
        level2Btn.gameObject.SetActive(false);
        level3Btn.gameObject.SetActive(false);
    }

    public void LoadLevel(int id){
        SceneLoader.loadLevelById(id);
    }


    public void  Position2 (){
		DisablePlayCampaign();
		CameraObject.SetFloat("Animate",1);
	}

	public void  Position1 (){
		CameraObject.SetFloat("Animate",0);
	}

    // Are You Sure - Quit Panel Pop Up
    public void AreYouSure()
    {
        PanelareYouSure.gameObject.SetActive(true);
        DisablePlayCampaign();
        DisavleSelectLevel();
    }

    public void No()
    {
        PanelareYouSure.gameObject.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }

    


}