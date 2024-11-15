using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Animator am;
    public float vidaMax = 100;
    float vidaActual;
    public BarraVida barraVida;

    void Start()
    {
        am = GetComponentInChildren<Animator>();
        vidaActual = vidaMax;
        barraVida.SetVidaMaxima(vidaMax);

    }
    public void recibirDamage(float damage)
    {
        // daño -= stats.armadura.Valor;
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
       
        vidaActual -= damage;

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
