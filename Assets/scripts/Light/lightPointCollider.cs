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
        if (accesa)
        {
            this.GetComponentInChildren<Light>().enabled = true;
        } else
        {
            this.GetComponentInChildren<Light>().enabled = false;
        }
    }
}
