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

    public float vidaActual;
    public float rangoAtaque;
    
    float tiempoProximoAtaque = 0f;

    private void Start()
    {
        am = GetComponent<Animator>();
        stats = GetComponent<EstadisticasPlayer>();
        Debug.Log(stats.vida.ValorBase);
        vidaActual = stats.vida.Valor;
        barraVida.SetVidaMaxima(stats.vida.Valor);
        rangoAtaque = stats.rangoAtaque.Valor;
}

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= tiempoProximoAtaque) {

            if (Input.GetMouseButtonDown(0))
            {
                ataque();
                tiempoProximoAtaque = Time.time + 1f / stats.velocidadAtaque.Valor;

            }
        }
    }

    void ataque() {
        am.SetTrigger("Ataque1");

        // Detectar enemigos en rango de ataque
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, stats.rangoAtaque.Valor, layerEnemigos);

        foreach (Collider2D enemigo in enemigosGolpeados) {
            Enemigo enemigoComponent = enemigo.GetComponent<Enemigo>();
            if (enemigoComponent != null)
            {
                enemigoComponent.recibirDamage(stats.ataque.Valor);
            }
        }
    }

    public void recibirDamage(float damage)
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
