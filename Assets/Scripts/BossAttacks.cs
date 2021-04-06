using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    EnemyMoviment EMScript;
    private GameObject localPlayer;
    private PlayerHealth pH;
    //Array que gere os 2 delays dos attacks.A primeira posição do array corresponde ao delay do ataque short range
    private float[] TimerForAttacks = new float[2] { 0f,0f};
    private Animator anim;
    private bool[] attacked=new bool[2];
    private bool isLocalOnTop;
    public LayerMask localLayer;
    public Transform topCheck;
   
    
    void Start()
    {
        EMScript = GetComponent<EnemyMoviment>();
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
        pH = localPlayer.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        //verificaçao da posiçao do palayer para começar a perseguiloo
        isLocalOnTop = Physics2D.OverlapCircle(topCheck.position, 0.2f, localLayer);
        if (TimerForAttacks[0] < 0 && EMScript.distanceFromPlayer.x<1.5f && EMScript.distanceFromPlayer.y < 0.2f && !isLocalOnTop)
        {
            SR_Attack();
            attacked[0] = false;
        }
        else
        {
            //Confirmaçao da posiçao do player e inicio da perceguiçao
            if (TimerForAttacks[0]<=1.3f)
                anim.SetBool("isSRAttacking",false);
            
            if (TimerForAttacks[0] <= 1.7f && TimerForAttacks[0] > 0f && !attacked[0])
            {
                //dano do ataque 0
                pH.Health -= 20;
                attacked[0] = true;
            }

            TimerForAttacks[0] -= Time.deltaTime;
        }
        if (TimerForAttacks[1] < 0 && EMScript.distanceFromPlayer.x < 3f && EMScript.distanceFromPlayer.y< 0.2f && !isLocalOnTop)
        {
            LR_Attack();
            attacked[1] = false;
        }
        else
        {
            //Confirmaçao da posiçao do player e inicio da perceguiçao
            if (TimerForAttacks[1] <= 3.5f)
                anim.SetBool("isLRAttacking", false);
            
            if (TimerForAttacks[1] <= 4f &&TimerForAttacks[1] > 0f &&!attacked[1])
            {
                //dano do ataque 1
                pH.Health -= 30;
                attacked[1] = true;
            }

            TimerForAttacks[1] -= Time.deltaTime;
        }
    }

    //tempo entre ataque (ataque 0)
    void SR_Attack()
    {
        anim.SetBool("isSRAttacking", true);
        localPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000f));
        TimerForAttacks[0] = 2f;
    }
    //tempo entre ataque (ataque 1)
    void LR_Attack()
    {
        anim.SetBool("isLRAttacking", true);
        TimerForAttacks[1] = 5f;
    }
}
