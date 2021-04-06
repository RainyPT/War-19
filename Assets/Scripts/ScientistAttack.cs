using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistAttack : MonoBehaviour
{
    EnemyMoviment EMScript;
    public GameObject potionPrefab;
    public Transform throwPoint;
    float TimerForNextAttack=3;
    private Transform localPlayer;
    private Animator anim;
    void Start()
    {
        
        EMScript = GetComponent<EnemyMoviment>();
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").transform;
        anim = GetComponent<Animator>();
    }

    //O update é feito por cada frame
    void Update()
    {
        //Tempo entre ataques
        if (TimerForNextAttack > 0)
        {
            anim.SetBool("isAttacking", false);
            TimerForNextAttack -= Time.deltaTime;
        }
        else if (TimerForNextAttack <= 0)
        {
            if (localPlayer != null)
            {
                if (EMScript.distanceFromPlayer.x < EMScript.lineOfSight&& EMScript.distanceFromPlayer.y < EMScript.lineOfSight)
                {
                    Instantiate(potionPrefab, throwPoint.position, throwPoint.rotation);
                    anim.SetBool("isAttacking", true);
                }
                TimerForNextAttack = 3;
            }
        }
       
    }

}
