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
    }

    // Update is called once per frame
    void Update()
    {
        if (!accesa)
        {
            gameObject.SetActive(false);
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
