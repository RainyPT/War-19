using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoviment : MonoBehaviour
{
    // Start is called before the first frame update
    private float MoveSpeed=4;
    private float jumpForce = 25f;
    private Rigidbody2D rigBody;
    private Animator anim;


    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        float moviment = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        anim.SetFloat("Speed", Mathf.Abs(moviment));
        transform.position += new Vector3(moviment, 0, 0) * Time.deltaTime * MoveSpeed;
        if(moviment!=0)
            Invert(moviment);
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("isJumping", true);
                rigBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                anim.SetBool("isJumping", false);
            }
            
        }
    }

    private void Invert(float moviment)
    {
        if (moviment < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
