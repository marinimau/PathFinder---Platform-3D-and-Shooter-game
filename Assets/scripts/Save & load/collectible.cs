using UnityEngine;
using System.Collections;

public class collectible : MonoBehaviour
{
    public bool active;
    public bool loaded;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        loaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded && active)
        {
            gameObject.SetActive(true);
            loaded = false;
            Debug.Log("collectible attivo");
        }
        if(loaded && !active){
            gameObject.SetActive(false);
            loaded = false;
            Debug.Log("collectible rimosso");
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

    }


}
