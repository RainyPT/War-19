using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    private float speed = 2;
    private GameObject player;
    private Rigidbody2D rigBody;
    public float lineOfSight = 5;
    public float lineOfRange = 2;
    public float distance = 2;
    public Vector2 distanceFromPlayer;
    public Animator anim;
    private bool isGrounded;
    public LayerMask groundLayer;
    private float rawDistanceX;
    public Transform groundCheck;
    private bool bossSpotted = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("LocalPlayer");
        anim = GetComponent<Animator>();
        rigBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Se o utilizador estiver a pisar o chão, isGrounded=true.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (player != null && isGrounded)
        {
            //calcula a distancia no eixo X entre o jogador local e o inimigo. Nota-se que esta distancia é sempre maior que 0
            distanceFromPlayer.x = Vector2.Distance(new Vector2(player.transform.position.x, 0), new Vector2(this.transform.position.x, 0));
            //calcula a distancia no eixo Y entre o jogador local e o inimigo. Nota-se que esta distancia é sempre maior que 0
            distanceFromPlayer.y = Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, this.transform.position.y));
            //calcula a distancia no eixo X entre o jogador local e o inimigo. No calculo desta distancia não é executado o módulo do resultado, logo o valor por ser negativo ou positivo.
            rawDistanceX = player.transform.position.x - this.transform.position.x;
            //inverte o model do inimigo consoante a sua direção
            Invert(rawDistanceX);

            //quando a distancia entre o inimigo e o localplayer for menor do que o seu raio de deteção
            if (distanceFromPlayer.x < lineOfSight)
            {
                //se o inimigo for um "Boss"
                if (gameObject.tag == "Boss")
                    bossSpotted = true;
                //se o inimigo for um "Scientist" e enquanto a distancia entre o inimigo e o localplayer for maior que o raio de "perseguição
                if (distanceFromPlayer.x > lineOfRange && gameObject.tag == "Scientist")
                {
                    //move o inimigo até ao jogador local
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
                }
                else
                {
                    if (gameObject.tag != "Boss")
                    {
                        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
                    }
                }
                //inicia a animação de andar se esta existir.
                anim.SetBool("isWalking", true);

            }
            else
            {
              anim.SetBool("isWalking", false);
            }
            //se o localplayer ja tenha sido detectador pelo inimigo boss
            if (gameObject.tag == "Boss" && bossSpotted)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }
        }
        //Se o inimigo já não estiver a pisar o chão. Evita cairem do mapa.
        if (!isGrounded)
        {
            //se tiver a olhar para a esquerda
            if (rawDistanceX < 0)
            {
                //empurra para a direita
                this.transform.position = new Vector2(this.transform.position.x+0.1f,this.transform.position.y);
            }
            else
            {
                //empurra para a esquerda
                this.transform.position = new Vector2(this.transform.position.x - 0.1f, this.transform.position.y);
            }
        }
            
    }
    private void Invert(float moviment)
    {
        if (moviment < 0f)
        {
            //inverte o modelo do inimigo se este tiver a olhar para esquerda
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
