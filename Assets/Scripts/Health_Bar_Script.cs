using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar_Script : MonoBehaviour
{
    public Slider slider;
    public Gradient cor;
    public Image Fill;

    //Inivializaçao da barra da vida com o hp no maximo
    public void SetMaxHealth(int health) {

        slider.maxValue = health;
        slider.value = health;

        Fill.color = cor.Evaluate(1f);
    }

    //Valor da vida e a sua cor (visual)
    public void setVida(int vida)
    {
        slider.value = vida;
        Fill.color = cor.Evaluate(slider.normalizedValue);
    }

}
