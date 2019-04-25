using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
    public AudioSource reloading;
    public AudioSource caduta;
    public AudioSource colpito;
    public AudioSource enemyDown;
    public AudioSource outOfAmmo;
    public AudioSource beep;

    public static int id;

    // Start is called before the first frame update
    void Start()
    {
        id = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(id!=-1){
            switch (id)
            {
                case 0:
                    reloading.Play();
                    break;
                case 1:
                    caduta.Play();
                    break;
                case 2:
                    colpito.Play();
                    break;
                case 3:
                    outOfAmmo.Play();
                    break;
                case 4:
                    enemyDown.Play();
                    break;
                case 5:
                    beep.Play();
                    break;
            }

            id = -1;
        }

    }
}
