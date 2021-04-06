using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    //caracteristicas do objeto bullet
    public float speed = 5f;
    private Rigidbody2D rb;
    private float TimeTillDestroy;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //força da bullet
        rb.velocity = transform.right * speed;
        //tempo de destruiçao se nao atingir qualquer objeto
        TimeTillDestroy = 2f;
    }

    //Funçao de confirmaçao de colisao
    void OnTriggerEnter2D (Collider2D hitinfo)
    {
        if (hitinfo.gameObject.tag == "Scientist" || hitinfo.gameObject.tag == "Bat" || hitinfo.gameObject.tag == "Ghost" || hitinfo.gameObject.tag == "Boss")
        {
            //damage que a bullet tira aos enemies
            hitinfo.gameObject.GetComponent<EnemyHealth>().enemyHealth -= 15;
        }
        //no final o objeto é apagado 
        Destroy(gameObject);
    }

    //Funçao de atualizaçao por frames do objeto ate este ser destruido (quer por tempo ou colisao)
    private void Update()
    {
       TimeTillDestroy -= Time.deltaTime;
        if (TimeTillDestroy <= 0)
        {
            Destroy(gameObject);
        }

      
    }

}
