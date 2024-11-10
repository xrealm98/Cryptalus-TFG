using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoEnemigo : MonoBehaviour
{
  
    private Transform objetivo;
    public Transform puntoAtaque;
    Animator am;
    public LayerMask layerPlayer;

    public float velocidadEnemigo = 3;
    public int dañoAtaque = 40;
    public float velocidadAtaque = 2f;
    float tiempoProximoAtaque = 3f;
    public float rangoAtaque = 0.75f;
    void Start()
    {
        am = GetComponent<Animator>();
        objetivo = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        am = GetComponent<Animator>();

        if (am.GetBool("estaMuerto"))
        {
            Debug.Log("Entro");
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            Debug.Log("Salgo");
        }
        else if (Vector2.Distance(transform.position, objetivo.position) > 1.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidadEnemigo * Time.deltaTime);
        }
        else {
            if (Time.time >= tiempoProximoAtaque)
            {

                    ataque();
                    tiempoProximoAtaque = Time.time + 3f / velocidadAtaque;

                
            }
        }
         
    }
    void ataque()
    {
        am.SetTrigger("Ataque1");

        // Detectar enemigos en rango de ataque
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, layerPlayer);

        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<CombateJugador>().recibirDaño(dañoAtaque);
        }
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
