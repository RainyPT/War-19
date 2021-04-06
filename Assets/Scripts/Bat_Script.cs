using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Script : MonoBehaviour
{
    // Movimento e ataque do inimigo bat.

    private Transform playerPos;
    private int speed = 2;
    private Vector2 distanceFromPlayer;
    public LayerMask localPlayerLayer;
    public Transform topCheck,biteCheck;
    private PlayerHealth pH;
    private float attackDelay;
    private bool spotted = false;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("LocalPlayer").transform;
        pH = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (playerPos != null)
        {
            distanceFromPlayer.x = Vector2.Distance(new Vector2(playerPos.position.x, 0), new Vector2(this.transform.position.x, 0));
            distanceFromPlayer.y = Vector2.Distance(new Vector2(0, playerPos.position.y), new Vector2(0, this.transform.position.y));
            Invert(playerPos.position.x - gameObject.transform.position.x);

            // Condição de verificção da posiçao do joagador 
            if (!spotted)
            {
                if (distanceFromPlayer.x < 6f)
                    spotted = true;
            }
            else
            {

                // Inicializaçao do movimento do morcego a perseguir o jogado
                if (distanceFromPlayer.x > 0.5f)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(playerPos.position.x, playerPos.position.y), speed * Time.deltaTime);
                }
            }

            // Condição qunado o jogador "pisa" o morcego
            if (Physics2D.OverlapCircle(topCheck.position, 0.1f, localPlayerLayer))
            {
                Destroy(gameObject);
            }

            // Condição do morcego estar a "colidir" com o jogador 
            if (Physics2D.OverlapCircle(biteCheck.position, 0.2f, localPlayerLayer))
            {

            //Ataque do morcego com o a subtraçao de hp no jogador
                if (attackDelay < 0)
                {
                    pH.Health -= 5;
                    // Tempo entre ataques
                    attackDelay = 0.5f;
                }
            }
             // Subtraçao do tempo de ataque, em tempo real
            attackDelay -= Time.deltaTime;
        }
    }

    //Função que inverte a "imagem" do morcego quando este vira de diraçao, no eixo x
    private void Invert(float moviment)
    {
        if (moviment < 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
