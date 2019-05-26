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
                    message.text = "[FUGA] I nemici hanno perso le tue tracce";
                    isShowing = true;
                    showTimer = 2f;
                    break;
                case 3:
                    message.text = "[IMMORTALE] Immortalità attiva";
                    isShowing = true;
                    showTimer = 2f;
                    break;
                case 4:
                    message.text = "[INVISIBILITA'] Invisibilità attiva";
                    isShowing = true;
                    showTimer = 2f;
                    break;
                case 5:
                    message.text = "[/INVISIBILITA'] Sei di nuovo visibile";
                    isShowing = true;
                    showTimer = 2f;
                    break;
                case 6:
                    message.text = "[/IMMORTALE] Sei di nuovo un mortale";
                    isShowing = true;
                    showTimer = 2f;
                    break;
                case 7:
                    message.text = "Missione fallita. Sei stato ucciso da un cecchino";
                    isShowing = true;
                    showTimer = 25f;
                    break;
                case 8:
                    message.text = "Sei morto";
                    isShowing = true;
                    showTimer = 25f;
                    break;
                case 9:
                    message.text = "Missione compiuta";
                    isShowing = true;
                    showTimer = 25f;
                    break;
                case 10:
                    message.text = "Missione fallita. Il boss ti ha ucciso";
                    isShowing = true;
                    showTimer = 25f;
                    break;
                case 11:
                    message.text = "[SUPER BULLET] Il danno della tua pistola è triplicato";
                    isShowing = true;
                    showTimer = 2f;
                    break;
                case 12:
                    message.text = "[/SUPER BULLET] Il danno della tua pistola è di nuovo normale";
                    isShowing = true;
                    showTimer = 2f;
                    break;
                case 13:
                    message.text = "Partita caricata";
                    isShowing = true;
                    showTimer = 2f;
                    break;
            }
        } else{
            showTimer -= Time.deltaTime*0.7f;
            if(showTimer<=0){
                isShowing = false;
                id = 0;
            }
        }

    }
}
