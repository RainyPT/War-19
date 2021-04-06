using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMoviment : MonoBehaviour
{

	private float MoveSpeed = 4;
	private float jumpForce = 25f;
	private float rollForce = 10f;
	public int pDirection;
	private Rigidbody2D rigBody;
	private Animator anim;
	private BoxCollider2D hitBox;
	private bool jumpKey = false;
	private bool rollKey = false;
	private float rollDelay = 0f;
	bool isGrounded,onTopOfEnemy;

	//Objectos necessários para fazer a deteção "isGrounded" e "onTopOfEnemy"
	public Transform groundCheck;
	public LayerMask groundLayer;
	public LayerMask enemyLayer;

	private void Start()
	{
		rigBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		hitBox = GetComponent<BoxCollider2D>();
	}

    private void Update()
    {
		//Detecta quando o utilizador clica para saltar(jump) ou para rolar(C) e muda o estado das variaveis rollKey e jumpKey
		if (Input.GetButtonDown("Jump"))
		{
			jumpKey = true;
		}
        if (Input.GetKeyDown(KeyCode.C))
        {
			rollKey = true;
        }
	}
	//Usamos este metodo para tornar o movimento mais estável e independente da qualidade do hardware do computador aonde o jogo está a ser jogado.
    private void FixedUpdate()
	{
		//moviment varia entre -1 e 1. Quando o utilizador clica na tecla "A" o valor caminha para o -1 e vice-versa quando clica "D"
		float moviment = Input.GetAxis("Horizontal");

		//torna o parametro "Speed" do animator da character local igual ao valor do moviment
		anim.SetFloat("Speed", Mathf.Abs(moviment));
		//muda os valores da posição do jogador local.Esta é a instrução que permite movimentar o character para os lados.
		transform.position += new Vector3(moviment, 0, 0) * Time.deltaTime * MoveSpeed;
		//Inverte o modelo do character consoante a direção que estiver a olhar(direita ou esquerda)
		if (moviment != 0)
		{
			Invert(moviment);
		}
		//Se o utilizador estiver a pisar o chão, isGrounded=true. Chama-se um groundcheck e serve para limitar o movimento do jogador(double jump...)
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
		//Se o utilizador estiver a pisar um inimigo, onTopOfEnemy=true. Permite ao character saltar enquanto tiver em cima de um inimigo
		onTopOfEnemy = Physics2D.OverlapCircle(groundCheck.position, 0.2f, enemyLayer);

		if (isGrounded|| onTopOfEnemy)
		{
			//Quando a tecla espaço fpr premida.
			if (jumpKey)
			{
				//Uma força vertical é exercida no character
				rigBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
				anim.SetBool("isJumping", true);
				jumpKey = false;
			}
			else
			{
				//quando o utilizador 
				anim.SetBool("isJumping", false);
			}

			roll();
		}
	}

	private void Invert(float moviment)
	{
		if (moviment < 0)
		{
			//inverte o modelo do character se este tiver a olhar para esquerda
			transform.localRotation = Quaternion.Euler(0, 180, 0);
			pDirection = 0;
		}
		else
		{
			transform.localRotation = Quaternion.Euler(0, 0, 0);
			pDirection = 1;
		}
	}
	//reverte a hitbox ao seu estado original depois do character acabar de fazer roll.
	private void resetHitbox()
	{
		hitBox.offset = new Vector2(hitBox.offset.x, -0.04016972f);
		hitBox.size = new Vector2(hitBox.size.x, 1.839661f);
	}

	
	private void roll()
	{
		if (rollKey && rollDelay < 0f)
		{
			rollKey = false;
			anim.SetBool("isRolling", true);
			//Muda a posição da hitbox.
			hitBox.offset = new Vector2(hitBox.offset.x, -0.54f);
			//Muda o tamanho da hitbox(faz mais pequena).
			hitBox.size = new Vector2(hitBox.size.x, 0.78f);
			//se o character tiver a olhar para a direita
			if (pDirection == 1)
			{
				//executa uma força "rollForce" para a direita
				rigBody.AddForce(new Vector2(rollForce, 0), ForceMode2D.Impulse);
			}
			else
			{
				//executa uma força "rollForce" para a esquerda
				rigBody.AddForce(new Vector2(-rollForce, 0), ForceMode2D.Impulse);
			}
			//Faz com que não seja possivel fazer varios rolls ao mesmo tempo. Por cada roll existe um delay de 2 segundos.
			rollDelay = 2f;
		}
		else
		{
            //permite acabar a animação do roll depois de esta completar
            if (rollDelay < 1.5f)
            {
				anim.SetBool("isRolling", false);
				resetHitbox();
			}

			rollKey = false;
		}
		//subtrai em tempo real o delay do roll.
		rollDelay -= Time.deltaTime;
	}
}