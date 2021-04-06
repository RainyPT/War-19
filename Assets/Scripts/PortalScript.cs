using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;


public class PortalScript : MonoBehaviour
{
    public static int currLevel=1;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        //Palavra-chave para ativaçao do portal
        actions.Add("next level", nextLevel);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;

    }

    //Reconhecimento de voz 
    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    //Transiçao de um nivel para o outro
    private void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
        if(currLevel!=3)
            currLevel++;
    }

    //Deteçao do player em cotecto com o portal para a permissao de reconhecimento do voz
    private void OnCollisionEnter2D(Collision2D collision)
    {
        keywordRecognizer.Start();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        keywordRecognizer.Stop();
    }
}
