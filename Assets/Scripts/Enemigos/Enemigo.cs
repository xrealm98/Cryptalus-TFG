using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Animator am;
    public int vidaMax = 100;
    int vidaActual;
    public BarraVida barraVida;

    void Start()
    {
        am = GetComponentInChildren<Animator>();
        vidaActual = vidaMax;
        barraVida.SetVidaMaxima(vidaMax);

    }
    public void recibirDaño(int daño)
    {
        vidaActual -= daño;

        barraVida.SetVida(vidaActual);
        am.SetTrigger("recibirGolpe");

        if (vidaActual <= 0)
        {
            muerteEnemigo();
        }
        
    }
    void muerteEnemigo() {
        am.SetBool("estaMuerto", true);
        GetComponent<Collider2D>().enabled = false;  
        this.enabled = false;
    }

}
