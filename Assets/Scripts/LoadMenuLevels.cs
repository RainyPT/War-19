using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Load de cada cene(menu/levels/endgame)
public class LoadMenuLevels : MonoBehaviour
{
    public void loadLevel1()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void loadLevel2()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
    public void loadLevel3()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
}
