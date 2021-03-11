using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    private float speed = 2;
    private GameObject player;
    public float lineOfSight = 5;
    public float distanceFromPlayer;
    public Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("LocalPlayer");
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (player != null)
        {
            distanceFromPlayer = Vector2.Distance(player.transform.position, this.transform.position);
            if (distanceFromPlayer < lineOfSight)
            {
                Invert(player.transform.position.x);
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                anim.SetBool("isWalking", true);

            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
    }
    private void Invert(float moviment)
    {
        if (moviment < 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
