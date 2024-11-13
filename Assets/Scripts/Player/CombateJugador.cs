using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class CombateJugador : MonoBehaviour
{
    public Animator am;
    public Transform puntoAtaque;
    public LayerMask layerEnemigos;
    public BarraVida barraVida;

    public float vidaMax = 100;
    public float vidaActual;
    public float rangoAtaque = 0.5f;
    public int dañoAtaque = 40;
    public float velocidadAtaque = 2f;
    float tiempoProximoAtaque = 0f;

    private void Start()
    {
        am = GetComponent<Animator>();
        vidaActual = vidaMax;
        barraVida.SetVidaMaxima(vidaMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= tiempoProximoAtaque) {

            if (Input.GetMouseButtonDown(0))
            {
                ataque();
                tiempoProximoAtaque = Time.time + 1f / velocidadAtaque;

            }
        }
    }

    void ataque() {
        am.SetTrigger("Ataque1");

        // Detectar enemigos en rango de ataque
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, layerEnemigos);

        foreach (Collider2D enemigo in enemigosGolpeados) {
            enemigo.GetComponent<Enemigo>().recibirDaño(dañoAtaque);
        }
    }
    public void recibirDaño(int daño)
    {
        vidaActual -= daño;

        barraVida.SetVida(vidaActual);

        am.SetTrigger("recibirGolpe");

        if (vidaActual <= 0)
        {
            muertePlayer();
        }

    }

    void muertePlayer()
    {
        am.SetBool("estaMuerto", true);
        GetComponent<Collider2D>().enabled = false;
        //SceneManager.LoadScene("Menu Principal");
        this.enabled = false;
        GetComponent<MovimientoJugador>().enabled = false;  
        
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
