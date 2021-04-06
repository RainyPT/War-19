using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private Animator anim;
    //tempo de explosao quendo triggered
    private float explodeTime = 0f;
    private bool explode = false;
    private PlayerHealth pH;
    private void Start()
    {
        anim = GetComponent<Animator>();
        pH = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerHealth>();
    }
    private void FixedUpdate()
    {
        if (explode)
        {
            if (explodeTime > 0f)
            {
                //Dano que a mina da ao player
                pH.Health -= 100;
            }
            else
            {
                //destruiçao do objeto mina apos a explosao
                Destroy(gameObject);
            }
            //tempo que demora a explodir e a desaparecer
            explodeTime -= Time.deltaTime;
        }
    }

    //Funçao de confirmaçao de colisao entre o player e a mina
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Confirmaçao e explosao da mina
        if (collision.gameObject.tag == "LocalPlayer")
        {
            anim.SetBool("exploded", true);
            explodeTime = 0.5f;
            explode = true;
        }
    }
}
