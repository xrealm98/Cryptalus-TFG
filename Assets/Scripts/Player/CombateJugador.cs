using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CombateJugador : MonoBehaviour
{
    public Animator am;
    public Transform puntoAtaque;
    public LayerMask layerEnemigos;

    public float rangoAtaque = 0.5f;
    public int dañoAtaque = 40;
    public float velocidadAtaque = 2f;
    float tiempoProximoAtaque = 0f;

    // Update is called once per frame
    void Update()
    {
        am = GetComponent<Animator>();

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
    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
