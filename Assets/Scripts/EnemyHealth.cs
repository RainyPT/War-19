using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;

    private void Start()
    {
        // vida de cada inimigos do jogo
        switch (gameObject.tag)
        {
            case "Ghost":
                enemyHealth = 20;
                break;
            case "Scientist":
                enemyHealth = 30;
                break;
            case "Bat":
                enemyHealth = 5;
                break;
             case "Boss":
                enemyHealth = 60;
                break;

        }
    }

    // Funçao de destruiçao de cada inimigo quando a sua vida chega a 0
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}
