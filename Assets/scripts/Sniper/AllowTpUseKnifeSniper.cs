using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowTpUseKnifeSniper : MonoBehaviour
{
    public GameObject sniper;
    bool allowKnife;
    public GameObject knife;
    public GameObject player;
    Animator anim;
    CharacterControllerScript controller;
    Sniper sniperController;
    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        allowKnife = false;
        controller = player.GetComponent<CharacterControllerScript>();
        sniperController = sniper.GetComponent<Sniper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allowKnife)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                StartCoroutine(OnAnimationComplete());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag.Equals("Player") && !sniperController.isDead && !CharacterControllerScript.isDead)
        {
            allowKnife = true;
            ShowMessage.id = 1;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            allowKnife = false;
            ShowMessage.id = 0;
        }

    }


    void deactivateKinife()
    {
        knife.SetActive(false);
    }

    IEnumerator OnAnimationComplete()
    {
        sniperController.stopEnemy();
        float knife_waittime = 1.00f;
        anim.SetBool("stabbing", true);
        knife.SetActive(true);
        controller.standStill();
        yield return new WaitForSeconds(knife_waittime);
        anim.SetBool("stabbing", false);
        knife.SetActive(false);
        sniperController.kill();
        controller.setStandardWalkSpeed();
        yield return true;
    }

}

