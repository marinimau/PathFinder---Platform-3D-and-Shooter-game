using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPointCollider : MonoBehaviour
{
    public bool accesa;
    public Light luce;
    // Start is called before the first frame update
    void Start()
    {
        accesa = true;
        luce = GetComponentInChildren<Light>().GetComponentInParent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!accesa)
        {
            luce.gameObject.SetActive(false);
        } else
        {
            luce.gameObject.SetActive(true);
        }
    }

    public void SaveLight()
    {
        Save.lightSave(this);
    }

    public void LoadLight()
    {
        LightData lightData = Save.lightLoad();

        /*---------------------------------------
         * 
         *  setto gli attributi con i valori caricati
         * 
         * --------------------------------------*/
        //accesa
        accesa = lightData.accesa;

    }


}
