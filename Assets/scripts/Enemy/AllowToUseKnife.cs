using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowToUseKnife : MonoBehaviour
{
    public GameObject enemy;
    bool allowKnife;
    public GameObject knife;
    public GameObject player;
    Animator anim;
    CharacterControllerScript controller;
    Patrol enemyController;
    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        allowKnife = false;
        controller = player.GetComponent<CharacterControllerScript>();
        enemyController = enemy.GetComponent<Patrol>();
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
        if (other.gameObject.tag.Equals("Player") && !enemyController.isDead && !CharacterControllerScript.isDead)
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
        enemyController.stopEnemy();
        Talk.id = 7;
        float knife_waittime = 1.00f;
        anim.SetBool("stabbing", true);
        knife.SetActive(true);
        controller.standStill();
        yield return new WaitForSeconds(knife_waittime);
        anim.SetBool("stabbing", false);
        knife.SetActive(false);
        enemyController.kill();
        controller.setStandardWalkSpeed();
        yield return true;
    }

}
