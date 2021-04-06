using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{


    public LayerMask localPlayerLayer;
    public Transform colisionArea;
    private PlayerHealth pH;
    private Rigidbody2D rigBody;
    private float attackDelay=0;
    
    //Deteçao de colisao com o jogador 
    private void Start()
    {
        pH = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerHealth>();
        rigBody = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Rigidbody2D>();
    }
    // O update é feito por cada frame
    void Update()
    {
        if (Physics2D.OverlapCircle(colisionArea.position, 0.2f, localPlayerLayer))
        {
            //Subtraçao de de vida no jogador
            if (attackDelay < 0)
            {
                pH.Health -= 5;
                attackDelay = 0.2f;
            }
        }
        //movimento de rotaçao da imagem 
        transform.Rotate(new Vector3(0, 0,10));
        attackDelay -= Time.deltaTime;
    }
}
