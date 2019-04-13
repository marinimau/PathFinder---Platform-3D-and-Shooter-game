using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{

    Vector3 idlePosition = new Vector3(0, 0, 0);
    Vector3 recoilPosition = new Vector3(0, 0, 0);
    float recoilTimer = 0f;
    public static bool recoilActive = false;
    Vector3 initialPosition;

    float RecoilUp;
    float RecoilBack;
    [Range(0.1f, 1f)]
    public float Grain = 1f;
    [Range(10f, 105f)]
    public float MuzzleVelocity = 105;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        RecoilUp = (Grain / 1000f);
        RecoilBack = (MuzzleVelocity / 105000f);

        // equivalent to: new Vector3(0, RecoilUp, RecoilBack);
        recoilPosition.y = RecoilUp;
        recoilPosition.z = RecoilBack;


        if (recoilActive)
        {
            // Pushing gun back (recoiling)
            recoilTimer += Time.deltaTime * 15; // recil is 2x as fast as recover
            if (recoilTimer > 1f)
            {
                recoilActive = false;
            }
        }
        else
        {
            // Pulling gun back to idle position (recovering)
            if (recoilTimer > 0f)
            {
                recoilTimer -= Time.deltaTime * 5;
            }
        }
        transform.localPosition = initialPosition + Vector3.Lerp(idlePosition, -recoilPosition, recoilTimer * recoilTimer); // recoil*recoil to smooth it out a bit
    }
}