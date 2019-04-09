using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class show_number : MonoBehaviour
{
    Text nColpi_show;
    // Start is called before the first frame update
    void Start()
    {
        nColpi_show = GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        nColpi_show.text=(GunScript.nColpi+1).ToString();
    }
}
