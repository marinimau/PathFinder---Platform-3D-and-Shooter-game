using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPointCollider : MonoBehaviour
{
    public bool accesa;
    public Light luce;
    public Vector3 startPos;
    public Vector3 hidePos;
    // Start is called before the first frame update
    void Start()
    {
        accesa = true;
        startPos = transform.position;
        hidePos = new Vector3(0, -100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (accesa)
        {
            if (transform.position != startPos)
            {
                transform.position=startPos;
            }   
        } else
        {
            if (transform.position != hidePos)
            {
               transform.position = hidePos;
            }
        }
    }
}
