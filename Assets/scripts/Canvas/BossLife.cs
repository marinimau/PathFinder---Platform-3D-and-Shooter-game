using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    public static bool show;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        show = false;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(show){
            canvas.enabled = true;
        } else{
            canvas.enabled = false;
        }
    }
}
