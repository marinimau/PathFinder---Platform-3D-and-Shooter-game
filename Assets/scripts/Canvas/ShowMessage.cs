using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour
{
    public static int id;
    public bool isShowing;
    public float showTimer;
    public Text message;
    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        isShowing = false;
        showTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShowing){
            switch (id)
            {
                case 0:
                    message.text = "";
                    break;
                case 1:
                    message.text = "Uccisione silenziosa <V>";
                    break;
                case 2:
                    message.text = "I nemici hanno perso le tue tracce";
                    isShowing = true;
                    showTimer = 1f;
                    break;
                case 3:
                    message.text = "Immortalità attiva";
                    isShowing = true;
                    showTimer = 1f;
                    break;
                case 4:
                    message.text = "Invisibilità attiva";
                    isShowing = true;
                    showTimer = 1f;
                    break;
                case 5:
                    message.text = "Sei di nuovo visibile";
                    isShowing = true;
                    showTimer = 1f;
                    break;
            }
        } else{
            showTimer -= Time.deltaTime*1;
            if(showTimer<=0){
                isShowing = false;
                id = 0;
            }
        }

    }
}
