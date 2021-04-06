using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int Health = 100;
    public Health_Bar_Script health_Bar;
    public float time2Respawn=2f;

    //Funçao de inicializaçao da health bar do jogador
    private void Start()
    {
        health_Bar.SetMaxHealth(Health);
    }

    //Funçao de destruiçao do jogador quando a vida do mesmo chega a 0
    public void Update()
    {

        if (Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        //Update do estado visual da healthbar de acordo com a "vida" do jogador
        health_Bar.setVida(Health);


    }


}
