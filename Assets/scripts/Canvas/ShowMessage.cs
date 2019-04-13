using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour
{
    public static int id;
    public Text message;
    // Start is called before the first frame update
    void Start()
    {
        id = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(id){
            case 0:
                message.text = "";
                break;
            case 1:
                message.text = "Uccisione silenziosa <V>";
                break;
        }
    }
}
