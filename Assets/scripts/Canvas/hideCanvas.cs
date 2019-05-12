using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideCanvas : MonoBehaviour
{
    Canvas[] canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentsInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            for(int i=0; i<canvas.Length; i++)
            {
                if(canvas[i]!=null)
                    canvas[i].enabled = false;
            }
        } else
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                if (canvas[i] != null){
                    if(CharacterControllerScript.isDead){
                        switch (canvas[i].tag)
                        {
                            case "Message":
                                canvas[i].enabled = true;
                                break;
                            default:
                                canvas[i].enabled = false;
                                break;
                        }
                    } else {
                        canvas[i].enabled = true;
                    }


                }
                    
            }
        }
    }
}
