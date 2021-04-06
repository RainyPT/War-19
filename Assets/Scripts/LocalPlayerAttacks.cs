using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
public class LocalPlayerAttacks : MonoBehaviour
{
    private Animator anim;
    private float[] timeBetweenAttacks = new float[2] { 0f, 0f };
    public Transform attackPoint;
    public float attackRange = 0.3f;
    public LayerMask enemyLayer;
    public GameObject bulletPrefab;
    EnemyHealth eH;
    private bool fired = false;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    void Start()
    {
        anim = GetComponent<Animator>();
        actions.Add("short", shortAttack);
        actions.Add("long", longAttack);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();
    }
    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }
    void Update()
    {
        //Tempo entre cada ataque
        if (timeBetweenAttacks[0] < 0.25f && fired)
        {
            Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
            fired = false;
        }
        if (timeBetweenAttacks[0] < 0f)
        {
            anim.SetBool("isLongAttacking", false);
        }
        if (timeBetweenAttacks[1] < 0.8f)
        {
            anim.SetBool("isShortAttacking", false);
        }
        timeBetweenAttacks[0] -= Time.deltaTime;
        timeBetweenAttacks[1] -= Time.deltaTime;
    }

    //Funçao do short attack
    void shortAttack()
    {
        if (timeBetweenAttacks[1] <= 0f)
        {
            anim.SetBool("isShortAttacking", true);
            //tempo entre o short attack
            timeBetweenAttacks[1] = 1f;
            //Confirmaçao de hit do objeto taque com o enemie
            Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);
            if (hitEnemy != null)
            {
                eH = hitEnemy.gameObject.GetComponent<EnemyHealth>();
                //Dmage do ataque
                eH.enemyHealth -= 30;
            }

        }
    }

    //Funçao do long attack
    void longAttack()
    {
        if (timeBetweenAttacks[0] <= 0f)
        {
            fired = true;
            anim.SetBool("isLongAttacking", true);
            timeBetweenAttacks[0] = 0.55f;
        }
    }
}

//Script sem voice recognition
/*

public class LocalPlayerAttacks : MonoBehaviour
{
    private Animator anim;
    private float[] timeBetweenAttacks = new float[2] { 0f, 0f };
    public Transform attackPoint;
    public float attackRange = 0.3f;
    public LayerMask enemyLayer;
    public GameObject bulletPrefab;
    EnemyHealth eH;
    private bool fired = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && timeBetweenAttacks[1] <= 0f)
        {
            anim.SetBool("isShortAttacking", true);
            timeBetweenAttacks[1] = 1f;
            Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);
            if (hitEnemy != null)
            {
                eH = hitEnemy.gameObject.GetComponent<EnemyHealth>();
                eH.enemyHealth -= 30;
            }

        }
        else
        {
            if (timeBetweenAttacks[1] < 0.8f)
            {
                anim.SetBool("isShortAttacking", false);
            }
        }

        if (Input.GetButtonDown("Fire1") && timeBetweenAttacks[0] <= 0f)
        {
            fired = true;
            anim.SetBool("isLongAttacking", true);
            timeBetweenAttacks[0] = 0.55f;
        }
        else
        {
            if (timeBetweenAttacks[0] < 0.25f && fired)
            {
                Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
                fired = false;
            }

            if (timeBetweenAttacks[0] < 0f)
            {
                anim.SetBool("isLongAttacking", false);
            }
        }
        timeBetweenAttacks[0] -= Time.deltaTime;
        timeBetweenAttacks[1] -= Time.deltaTime;
    }
}


 */