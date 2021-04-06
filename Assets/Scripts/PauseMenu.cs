using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool pause = false;
    public GameObject InGamePause;
    void Update()
    {
        //Ativaçao do ingame manu atravez do "esc"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //menu pop up no primeiro clique de "esc"
            if (pause == false)
            {

                PauseTheGame();
            }
            //resumo do jogo com um segundo clique no "esc"
            else {

                ResumeTheGame();
            }

        }
    }

    public void PauseTheGame()
    {

        InGamePause.SetActive(true);
        Time.timeScale = 0f;
        pause = true;

    
    }
     // resumo do jogo clicando na opçao RESUME
    public void ResumeTheGame()
    {
        InGamePause.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
        Debug.Log("here");

    }

    //Load da cene menu principal ao clicar na opçao MENU
    public void GoMenu()
    {
        InGamePause.SetActive(false);
        pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    //quit do jogo na opaço QUIT
    public void GoQuit()
    {
        Application.Quit();
    }

}
