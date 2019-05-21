using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allowToUseKnifeBoris : MonoBehaviour
{
    public GameObject boris;
    bool allowKnife;
    public GameObject knife;
    public GameObject player;
    Animator anim;
    CharacterControllerScript controller;
    Boris borisController;
    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        allowKnife = false;
        controller = player.GetComponent<CharacterControllerScript>();
        borisController = boris.GetComponent<Boris>();
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
        if (other.gameObject.tag.Equals("Player") && !borisController.isDead && !CharacterControllerScript.isDead)
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
        borisController.stopEnemy();
        Talk.id = 7;
        float knife_waittime = 1.00f;
        anim.SetBool("stabbing", true);
        knife.SetActive(true);
        controller.standStill();
        yield return new WaitForSeconds(knife_waittime);
        anim.SetBool("stabbing", false);
        knife.SetActive(false);
        borisController.bodyHit=true;
        controller.setStandardWalkSpeed();
        borisController.startMoving();
        yield return true;
    }

}
