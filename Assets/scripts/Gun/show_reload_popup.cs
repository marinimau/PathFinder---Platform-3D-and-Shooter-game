using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class show_reload_popup : MonoBehaviour
{
    bool armaScarica = false;
    static int nColpi = 5;
    public GameObject reload_popup;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(nColpi==0){
            armaScarica = true;
        }
        if(Input.GetButtonDown("Fire1")){
            if(!armaScarica){
                nColpi=nColpi-1;
                Debug.Log("numero di colpi:" + nColpi);
            } 
        }
        if(Input.GetKey(KeyCode.R)){
            nColpi = 14;
            armaScarica = false;
        }
        if(armaScarica){
            reload_popup.SetActive(true);
        }
        else{
            reload_popup.SetActive(false);
        }
    }
}
