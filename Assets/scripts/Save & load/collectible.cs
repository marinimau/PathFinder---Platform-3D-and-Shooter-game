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
        loaded = false;
        moved = false;
        oldPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (loaded && active)
        {
            loaded = false;
            Debug.Log("collectible attivo");
        }
        if (loaded && !active)
        {
            loaded = false;
            Debug.Log("collectible rimosso");
        }
        if (!active && !moved)
        {
            gameObject.transform.position = new Vector3(0, -100, 0);
            moved = true;
        }
        if (active && transform.position != oldPosition)
        {
            transform.position = oldPosition;

        }
    }

    public void SaveCollectible()
    {
        Save.collectibleSave(this);
    }

    public void LoadCollectible()
    {
        //SceneManager.LoadScene("playground", LoadSceneMode.Single);
        CollectibleData collectible = Save.collectibleLoad();

        //active
        active = collectible.active;
        loaded = true;
        moved = false;

    }


}
