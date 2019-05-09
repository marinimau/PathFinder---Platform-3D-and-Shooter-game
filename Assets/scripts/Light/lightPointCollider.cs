using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPointCollider : MonoBehaviour
{
    public bool accesa;
    private Light luce;
    private bool prev;
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
            gameObject.active = false;
        } else
        {
            gameObject.active = true;
        }
    }


}
