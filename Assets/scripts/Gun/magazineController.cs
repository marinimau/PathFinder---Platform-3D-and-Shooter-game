using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magazineController : MonoBehaviour
{
    static float startWidth = 350;
    float currentWidth = startWidth;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentWidth = (startWidth / (14)) * (GunScript.nColpi+1);
        rectTransform.sizeDelta = new Vector2(currentWidth, 50);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !GunScript.armaScarica){
            currentWidth =(startWidth/(14))*GunScript.nColpi;
            rectTransform.sizeDelta = new Vector2(currentWidth, 50);

        }

        if(Input.GetKey(KeyCode.R)){
            //non uso startWidth per gestire i due casi di ricarica
            currentWidth = (startWidth / (14)) * (GunScript.nColpi+1);
            rectTransform.sizeDelta = new Vector2(currentWidth, 50);
        }
    }
}
