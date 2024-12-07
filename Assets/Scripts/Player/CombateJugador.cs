using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class CombateJugador : MonoBehaviour
{
    public Animator am;
    public Transform puntoAtaque;
    public LayerMask layerEnemigos;
    public BarraVida barraVida;

    public EstadisticasPlayer stats;

    public float vidaMaxima;
    public float vidaActual;
    public float rangoAtaque;
    public int monedas;
    
    float tiempoProximoAtaque = 0f;

    private void Start()
    {
        am = GetComponent<Animator>();
        stats = GetComponent<EstadisticasPlayer>();

        vidaActual = stats.vida.Valor;
        vidaMaxima = stats.vida.Valor;
        barraVida.SetVidaMaxima(stats.vida.Valor);
        rangoAtaque = stats.rangoAtaque.Valor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= tiempoProximoAtaque) {

            if (Input.GetMouseButtonDown(0))
            {
                am.SetTrigger("Ataque1");
                tiempoProximoAtaque = Time.time + 1f / stats.velocidadAtaque.Valor;

            }
        }
    }

    void Ataque() {
        // Detectar enemigos en rango de ataque
       Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, stats.rangoAtaque.Valor, layerEnemigos);

        foreach (Collider2D enemigo in enemigosGolpeados) {
            Enemigo enemigoComponent = enemigo.GetComponent<Enemigo>();
            if (enemigoComponent != null)
            {
                // Si es un trigger, no infligir daño, saltar al siguiente enemigo
                if (enemigo.isTrigger)
                {
                    continue; 
                }
                enemigoComponent.RecibirDamage(stats.ataque.Valor);
            }
        }
    }

    public void RecibirDamage(float damage)
    {
        damage -= stats.armadura.Valor;
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        vidaActual -= damage;
     
        barraVida.SetVida(vidaActual);

        am.SetTrigger("recibirGolpe");

        if (vidaActual <= 0)
        {
            muertePlayer();
        }

    }

    public void curarVida(float cantidad) {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMaxima);
        barraVida.SetVida(vidaActual);

    }

    void muertePlayer()
    {
        am.SetBool("estaMuerto", true);
        GuardadoManager.instancia.GuardarDatos();
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
