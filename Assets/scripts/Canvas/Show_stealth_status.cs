using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_stealth_status : MonoBehaviour
{
    public static int icon=0;
    RectTransform rectTransform;
    // Start is called before the first frame update
    public GameObject stealth_icon;
    public GameObject not_stealth_icon;
    public GameObject warning_icon;

    public RectTransform stealth_iconRT;
    public RectTransform not_stealth_iconRT;
    public RectTransform warning_iconRT;


    void Start()
    {
        stealth_iconRT = stealth_icon.GetComponent<RectTransform>();
        not_stealth_iconRT = not_stealth_icon.GetComponent<RectTransform>();
        warning_iconRT = warning_icon.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(icon){
            case 0:

                stealth_iconRT.sizeDelta = new Vector2(180,180);
                not_stealth_iconRT.sizeDelta = new Vector2(0, 0);
                warning_iconRT.sizeDelta = new Vector2(0, 0);
                break;
            case 1:
                stealth_iconRT.sizeDelta = new Vector2(0, 0);
                not_stealth_iconRT.sizeDelta = new Vector2(180, 180);
                warning_iconRT.sizeDelta = new Vector2(0, 0);
                break;
            case 2:
                stealth_iconRT.sizeDelta = new Vector2(0, 0);
                not_stealth_iconRT.sizeDelta = new Vector2(0, 0);
                warning_iconRT.sizeDelta = new Vector2(180, 180);
                break;
        }


    }
}
