using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Animator am;
    public int vidaMaxima = 100;
    int vidaActual;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponentInChildren<Animator>();
        vidaActual = vidaMaxima;

    }
    public void recibirDaño(int daño)
    {
        vidaActual -= daño;

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
