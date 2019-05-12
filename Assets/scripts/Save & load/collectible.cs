using UnityEngine;
using System.Collections;

public class collectible : MonoBehaviour
{
    public bool active;
    public bool loaded;
    public bool moved;
    public Vector3 oldPosition;
    public Vector3 hidePosition = new Vector3(0, -100, 0);

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        oldPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            if (transform.position != hidePosition)
            {
                transform.position = hidePosition;
            }
        }
        else
        {
            if (transform.position != oldPosition)
            {
                transform.position = oldPosition;
            }
        }
    }
}
