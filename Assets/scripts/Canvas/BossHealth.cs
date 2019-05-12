using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public static Text health;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = Boss.life.ToString();
    }
}
