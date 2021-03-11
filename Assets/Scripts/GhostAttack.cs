using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    EnemyMoviment EMScript;
    private GameObject localPlayer;
    float TimerForNextAttack = 1;
    private PlayerHealth pH;
    float lineOfAttack = 0.9f;
    void Start()
    {
        EMScript = GetComponent<EnemyMoviment>();
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
        pH = localPlayer.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerForNextAttack > 0)
        {
            if (!EMScript.anim.GetBool("inRange"))
                EMScript.anim.SetBool("inRange", false);
            TimerForNextAttack -= Time.deltaTime;
        }
        else if (TimerForNextAttack <= 0)
        {
            if (localPlayer != null)
            {
                if (EMScript.distanceFromPlayer < lineOfAttack)
                {
                    EMScript.anim.SetBool("inRange", true);
                    TimerForNextAttack = 1;
                    pH.Health -= 20;
                }
                else
                {
                    EMScript.anim.SetBool("inRange", false);
                }
            }
        }
    }
}
