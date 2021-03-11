using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInteraction : MonoBehaviour
{
    private Rigidbody2D rigBody;
    private float playerXSignal;
    private PlayerHealth pH;
    private Animator anim;
    public void Awake()
    {
        rigBody = GetComponent<Rigidbody2D>();
        playerXSignal = GameObject.FindGameObjectWithTag("LocalPlayer").transform.position.x;
        anim = GetComponent<Animator>();
        if (playerXSignal > 0)
        {
            rigBody.AddForce(new Vector2(6f,5f), ForceMode2D.Impulse);
        }
        else
        {
            rigBody.AddForce(new Vector2(-6f, 5f), ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "LocalPlayer")
        {
            pH = col.gameObject.GetComponent<PlayerHealth>();
            pH.Health -= 50;
        }
        anim.SetBool("isGrounded", true);
        Destroy(gameObject);

    }
}
