using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//load de cada sene a partir de do menu inicial
public class MenuInicial : MonoBehaviour
{
    public GameObject Controller;

    //Load do primeiro nivel
    public void PlayGame()
    {
        SceneManager.LoadScene(PortalScript.currLevel+1, LoadSceneMode.Single);
    }
    //Load das cene opcions 
    public void playSettingsScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    //Load da cena anterior atravez do botao back
    public void back()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    //Load da cena anterior atravez do botao back
    public void BackOptions() 
    {
        Controller.SetActive(false);
    }
    //Load das options
    public void Options()
    {
        Controller.SetActive(true);
    }

    //Quit game 
    public void QuitGame()
    {
        Application.Quit();
    }
}
