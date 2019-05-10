using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestisciLight : MonoBehaviour
{

    private lightPointCollider lpc;
    private Light light;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        lpc = GetComponentInParent<lightPointCollider>();
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(lpc.accesa){
            transform.position = startPos;
        } else {
            transform.position = new Vector3(0, -100, 0);
        }
    }
}
