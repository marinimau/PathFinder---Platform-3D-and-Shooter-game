using UnityEngine;
using System.Collections;

public class collectible : MonoBehaviour
{
    public bool active;
    public bool loaded;
    public bool moved;
    public Vector3 oldPosition;

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
            gameObject.SetActive(false);
        }
    }

    public void SaveCollectible()
    {
        Save.collectibleSave(this);
    }

    public void LoadCollectible()
    {
        CollectibleData collectible = Save.collectibleLoad();

        //active
        active = collectible.active;

    }


}
