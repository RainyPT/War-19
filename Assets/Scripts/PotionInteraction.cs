using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInteraction : MonoBehaviour
{
    private Rigidbody2D rigBody;
    private float playerXSignal;
    private PlayerHealth pH;
    private Animator anim;
    private float destroyDelay= 0.75f;
    private bool collided = false;
    //quando a potion aparece, esta função é executada
    public void Awake()
    {
        rigBody = GetComponent<Rigidbody2D>();
        playerXSignal = GameObject.FindGameObjectWithTag("LocalPlayer").transform.position.x-gameObject.transform.position.x;
        anim = GetComponent<Animator>();
        pH = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerHealth>();
        //se o character tiver á direita do "Villain"
        if (playerXSignal > 0)
        {
            //É executada uma força na potion para a direita
            rigBody.AddForce(new Vector2(6f,5f), ForceMode2D.Impulse);
        }
        else
        {
            //É executada uma força na potion para a esquerda
            rigBody.AddForce(new Vector2(-6f, 5f), ForceMode2D.Impulse);
        }
    }
    public void Update()
    {
        //se houver colisão com alguma coisa
        if (collided)
        {
            //existe um timer de 750 ms até ela explodir. Esta instrução serve para começar a subtrair o valor do destroyDelay.
            destroyDelay -= Time.deltaTime;
        }
        //se o delay for menor ou igual que 0
        if (destroyDelay <= 0)
        {
            //destroy a poção
            Destroy(gameObject);
        }
    }
    //quando a potion colidir com alguma coisa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cesa todo o movimento do modelo
        rigBody.bodyType = RigidbodyType2D.Static;
        //animação de explosão
        anim.SetBool("isGrounded", true);
        collided = true;
        //se atingir o character principal
        if (collision.gameObject.tag == "LocalPlayer")
        {
            //tira 10 de vida
            pH.Health -= 10;
        }
    }
}
