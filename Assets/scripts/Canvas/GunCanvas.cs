using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCanvas : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterControllerScript.isDead){
            canvas.enabled = false;
        }
    }
}
