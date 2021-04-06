using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    //Load da cena dos levels(menu)
    private void Start()
    {
        loadPassedLevels();
    }

    //Confirmaçao e desbloquear de cada nivel
    public void loadPassedLevels()
    {
        for (int i = 1; i <= PortalScript.currLevel; i++)
        {
            if (this.name == "Level " + i)
            {
                //desbloequeio de cada nivel
                this.GetComponent<Button>().interactable = true;
            }

        }
    }
}
