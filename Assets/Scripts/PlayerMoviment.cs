using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoviment : MonoBehaviour
{
    // Start is called before the first frame update
    private float MoveSpeed=4;
    private float jumpForce = 25f;
    private float rollForce = 10f;
    private bool jumped = false;
    private int pDirection;
    private Rigidbody2D rigBody;
    private Animator anim;
    private BoxCollider2D hitBox;


    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        hitBox = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rigBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping", false);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                //manageRolling();
                roll();
            }
        }
    }
    private void FixedUpdate()
    {
        float moviment = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moviment));
        transform.position += new Vector3(moviment, 0, 0) * Time.deltaTime * MoveSpeed;
        if (moviment != 0)
            Invert(moviment);
    }


    private void Invert(float moviment)
    {
        if (moviment < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            pDirection = 0;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            pDirection = 1;
        }
    }
    private void resetHitbox()
    {
        hitBox.offset = new Vector2(hitBox.offset.x, -0.04016972f);
        hitBox.size = new Vector2(hitBox.size.x, 1.839661f);
    }
    private void roll()
    {
        anim.SetBool("isRolling", true);
        hitBox.offset = new Vector2(hitBox.offset.x, -0.54f);
        hitBox.size = new Vector2(hitBox.size.x, 0.78f);
        if (pDirection == 1)
        {
            rigBody.AddForce(new Vector2(rollForce, 0), ForceMode2D.Impulse);
        }
        else
        {
            rigBody.AddForce(new Vector2(-rollForce, 0), ForceMode2D.Impulse);
        }
        
        resetHitbox();
    }
}
