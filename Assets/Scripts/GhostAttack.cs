using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    //Movimento 
    EnemyMoviment EMScript;
    private GameObject localPlayer;
    //Delay entre cada ataque
    float TimerForNextAttack = 1;
    private PlayerHealth pH;
    float lineOfAttack = 1f;
    void Start()
    {
        EMScript = GetComponent<EnemyMoviment>();
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
        pH = localPlayer.GetComponent<PlayerHealth>();
        
    }

    //Funçao de ataque
    public void Update()
    {
        if (TimerForNextAttack > 0)
        {
            TimerForNextAttack -= Time.deltaTime;
        }
        else if (TimerForNextAttack <= 0)
        {
            //Confirmaçao se o player esta dentro da "area de ataque"
            EMScript.anim.SetBool("inRange", false);
            if (localPlayer != null && EMScript.distanceFromPlayer.x!=0 && EMScript.distanceFromPlayer.y!=0)
            {
                // Movimento do cientista atras do player
                if (EMScript.distanceFromPlayer.x < lineOfAttack && EMScript.distanceFromPlayer.y < lineOfAttack)
                {
                    EMScript.anim.SetBool("inRange", true);
                    //Tem ate ao proximo ataque
                    TimerForNextAttack = 1;
                    //Damage do ataque
                    pH.Health -= 5;
                }
            }
        }
    }

}
