using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class show_reload_popup : MonoBehaviour
{

    public GameObject reload_popup;
    public Text popupText;

    // Start is called before the first frame update
    void Start()
    {
        popupText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        if(GunScript.armaScarica){
            popupText.text = "Reload!";

        }
        else{
            popupText.text = "";
        }
    }

}
