using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image relleno;
    public enum TipoBarra { Jugador, Enemigo }
    
    public TipoBarra tipoBarra;

    public void SetVidaMaxima(float vida) {
        slider.maxValue = vida;
        slider.value = vida;

        if (tipoBarra == TipoBarra.Jugador) {
            relleno.color = gradient.Evaluate(1f);
        }

        
    }

    public void SetVida(float vida) { 
    slider.value = vida;
        if (tipoBarra == TipoBarra.Jugador)
        {
            relleno.color = gradient.Evaluate(slider.normalizedValue);
        }
           
    }
}
