using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum EstadoEnemigo
{
    Idle,
    Seguimiento,
    Atacando

};
public class ComportamientoEnemigo : MonoBehaviour
{
    public Transform puntoAtaque;
    private Rigidbody2D rb;
    private Animator am;
    private EstadoEnemigo estadoEnemigo;

    public LayerMask layerPlayer;
    private Transform player;


    private float tiempoProximoAtaqueColdoown;

    public string nombreEnemigo;
    public EstadisticasBase vida;
    public EstadisticasBase ataque;
    public EstadisticasBase armadura;
    public EstadisticasBase velocidadMovimiento;
    public EstadisticasBase velocidadAtaque;
    private float rangoAtaque;

    private bool mirandoDireccion = true;

    // Circulo de detecci�n de player donde el enemigo empezara a perseguir.
    public float rangoDetector;
    public Transform puntoDetector;

    public EnemigoDatos enemigoDatosSO;
    public EstadisticasPlayer estadisticasPlayer;

    private bool playerEstaEnRango = false;

    void Start()
    {
        am = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        CambioEstado(EstadoEnemigo.Idle);

        estadisticasPlayer = GameObject.Find("Player").GetComponent<EstadisticasPlayer>();

        IniciarEstadisticas();


    }

    // M�todo que inicializa las estadisticas del enemigo y las escala seg�n el nivel del jugador.
    public void IniciarEstadisticas()
    {
        nombreEnemigo = enemigoDatosSO.nombreTipoEnemigo;
        vida.ValorBase = enemigoDatosSO.vida;
        ataque.ValorBase = enemigoDatosSO.ataque;
        armadura.ValorBase = enemigoDatosSO.armadura;
        velocidadMovimiento.ValorBase = enemigoDatosSO.velocidadMovimiento;
        velocidadAtaque.ValorBase = enemigoDatosSO.velocidadAtaque;
        rangoAtaque = enemigoDatosSO.rangoAtaque;
        rangoDetector = enemigoDatosSO.rangoDetencion;


        float escalarEstadisticas = estadisticasPlayer.nivelPlayer * 0.05f;

        ataque.addModificador(new ModificadorEstadisticas(escalarEstadisticas, TipoModificadorEstadistica.PorcentajeMult, this));
        armadura.addModificador(new ModificadorEstadisticas(escalarEstadisticas, TipoModificadorEstadistica.PorcentajeMult, this));
        vida.addModificador(new ModificadorEstadisticas(escalarEstadisticas, TipoModificadorEstadistica.PorcentajeMult, this));

        //Debug.Log("Ataque: " + ataque.Valor);
        //Debug.Log("Armadura: " + armadura.Valor);
        //Debug.Log("Vida: " + vida.Valor);


    }

    void Update()
    {
        if (am.GetBool("estaMuerto"))
        {
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            return;

        }
        CheckJugador();
        if (tiempoProximoAtaqueColdoown > 0)
        {
            tiempoProximoAtaqueColdoown -= Time.deltaTime;
        }
        if (estadoEnemigo == EstadoEnemigo.Seguimiento)
        {
            Seguimiento();
        }
        else if (estadoEnemigo == EstadoEnemigo.Atacando)
        {
            rb.velocity = Vector2.zero;
        }

    }

    // M�todo para seguir al jugador.
    void Seguimiento()
    {
        if (playerEstaEnRango)
        {
            if (tiempoProximoAtaqueColdoown <= 0)
            {
                CambioEstado(EstadoEnemigo.Atacando);
            }
            else
            {
                CambioEstado(EstadoEnemigo.Idle);
            }
            return;
        }

        if (player.position.x < transform.position.x && mirandoDireccion ||
                   player.position.x > transform.position.x && !mirandoDireccion)
        {
            Flip();
        }
        Vector2 direccion = (player.position - transform.position).normalized;
        rb.velocity = direccion * velocidadMovimiento.Valor;

    }

    // M�todo que interactua con el punto de detenci�n para tener un rango de agresividad del enemigo.
    private void CheckJugador()
    {
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoDetector.position, rangoDetector, layerPlayer);
        if (enemigosGolpeados.Length > 0)
        {

            player = enemigosGolpeados[0].transform;
            CheckRangoAtaque();

            if (playerEstaEnRango && tiempoProximoAtaqueColdoown <= 0)
            {
                tiempoProximoAtaqueColdoown = velocidadAtaque.Valor;
                CambioEstado(EstadoEnemigo.Atacando);

            }
            else if (!playerEstaEnRango && estadoEnemigo != EstadoEnemigo.Atacando)
            {
                CambioEstado(EstadoEnemigo.Seguimiento);

            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            CambioEstado(EstadoEnemigo.Idle);

        }
    }

    private void CheckRangoAtaque()
    {

        Collider2D[] enemigosEnRango = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, layerPlayer);
        playerEstaEnRango = enemigosEnRango.Length > 0;
    }
    // M�todo donde cambiamos el estado del enemigo seg�n su acci�n actual.
    public void CambioEstado(EstadoEnemigo nuevoEstado)
    {

        // Salir de la animaci�n actual
        if (estadoEnemigo == EstadoEnemigo.Idle)
        {
            am.SetBool("enIdle", false);
        }
        else if (estadoEnemigo == EstadoEnemigo.Seguimiento)
        {
            am.SetBool("enSeguimiento", false);
        }
        else if (estadoEnemigo == EstadoEnemigo.Atacando)
        {
            am.SetBool("enAtaque", false);
        }

        // Actualizamos el estado actual y ponemos la nueva animaci�n 
        estadoEnemigo = nuevoEstado;

        if (estadoEnemigo == EstadoEnemigo.Idle)
        {
            am.SetBool("enIdle", true);
        }
        else if (estadoEnemigo == EstadoEnemigo.Seguimiento)
        {
            am.SetBool("enSeguimiento", true);
        }
        else if (estadoEnemigo == EstadoEnemigo.Atacando)
        {
            am.SetBool("enAtaque", true);
        }

    }

    // M�todo para hacer da�o al jugador en caso de estar en el punto de ataque. (Se llama desde la animaci�n)
    public void AtaqueAlJugador()
    {
        // Detectar enemigos en rango de ataque
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, layerPlayer);

        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<CombateJugador>().RecibirDamage(ataque.Valor);
        }
    }
    // M�todo para hacer flip al eje del objeto.
    void Flip()
    {
        mirandoDireccion = !mirandoDireccion;
        // Cambiar escala del enemigo
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    }

    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
        Gizmos.DrawWireSphere(puntoDetector.position, rangoDetector);
    }

}

