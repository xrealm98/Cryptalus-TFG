using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ComportamientoEnemigo : MonoBehaviour
{

    public Transform enemigoGFX;
    public Transform player;
    public Transform puntoAtaque;
    private GameObject hitbox;
    Animator am;
    public LayerMask layerPlayer;

    float tiempoProximoAtaque = 3f;

    public string nombreEnemigo;
    public EstadisticasBase vida;
    public EstadisticasBase ataque;
    public EstadisticasBase armadura;
    public EstadisticasBase velocidadMovimiento;
    public EstadisticasBase velocidadAtaque;
    public float rangoAtaque;


    private bool mirandoDerecha = true;
    public float distanciaProximoPunto = 1f;
    private bool estaPersiguiendo;

    Path path;
    int puntoActual;
    bool puntoAlcanzado = false;
    Seeker seeker;
    Rigidbody2D rb;

    public EnemigoDatos enemigoDatosSO;
    public EstadisticasPlayer estadisticasPlayer;

    private float posicionInicialXHitbox;

    void Start()
    {
        estadisticasPlayer = GameObject.Find("Player").GetComponent<EstadisticasPlayer>();
        am = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hitbox = transform.Find("HitboxGolpe").gameObject;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        posicionInicialXHitbox = hitbox.transform.localPosition.x;

        IniciarEstadisticas();
        InvokeRepeating("ActualizarCamino", 0f, .5f);


    }

    public void IniciarEstadisticas()
    {
        nombreEnemigo = enemigoDatosSO.nombreTipoEnemigo;
        vida.ValorBase = enemigoDatosSO.vida;
        ataque.ValorBase = enemigoDatosSO.ataque;
        armadura.ValorBase = enemigoDatosSO.armadura;
        velocidadMovimiento.ValorBase = enemigoDatosSO.velocidadMovimiento;
        velocidadAtaque.ValorBase = enemigoDatosSO.velocidadAtaque;
        rangoAtaque = enemigoDatosSO.rangoAtaque;


        float calculo = estadisticasPlayer.nivelPlayer * 0.05f;

        ataque.addModificador(new ModificadorEstadisticas(calculo, TipoModificadorEstadistica.PorcentajeMult, this));
        armadura.addModificador(new ModificadorEstadisticas(calculo, TipoModificadorEstadistica.PorcentajeMult, this));
        vida.addModificador(new ModificadorEstadisticas(calculo, TipoModificadorEstadistica.PorcentajeMult, this));

        Debug.Log("Ataque: " + ataque.Valor);
        Debug.Log("Armadura: " + armadura.Valor);
        Debug.Log("Vida: " + vida.Valor);


    }


    void ActualizarCamino()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.position, CaminoCompletado);
        }
    }

    void CaminoCompletado(Path p)
    {
        if (!p.error)
        {
            path = p;
        }

    }
    void Update()
    {

        rb.AddForce(Vector2.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player == null)
            {
                player = collision.transform;
            }
            estaPersiguiendo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            am.SetBool("movimiento", false);
            rb.AddForce(Vector2.zero);
            estaPersiguiendo = false;
        }
    }


    void FixedUpdate()
    {
        if (am.GetBool("estaMuerto"))
        {
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

        }

        if (path == null)
        {
            return;
        }

        if (estaPersiguiendo)
        {
            if (puntoActual >= path.vectorPath.Count)
            {
                am.SetBool("movimiento", false);
                if (Time.time >= tiempoProximoAtaque)
                {
                    am.SetTrigger("Ataque1");
                    tiempoProximoAtaque = Time.time + 3f / velocidadAtaque.Valor;
                }
                puntoAlcanzado = true;
                return;
            }
            else
            {
                puntoAlcanzado = false;
            }

            Vector2 direccion = ((Vector2)path.vectorPath[puntoActual] - rb.position).normalized;

            Vector2 fuerza = direccion * velocidadMovimiento.Valor * Time.deltaTime;

            rb.AddForce(fuerza);

            float distancia = Vector2.Distance(rb.position, path.vectorPath[puntoActual]);

            if (distancia < distanciaProximoPunto)
            {
                puntoActual++;

            }

            ComprobarMovimiento(fuerza);
        }

    }

    void ComprobarMovimiento(Vector2 fuerza)
    {
        if (fuerza.magnitude >= 0.01f)
        {
            am.SetBool("movimiento", true);

            // Flip según la dirección
            if ((fuerza.x > 0 && !mirandoDerecha) || (fuerza.x < 0 && mirandoDerecha))
            {
                Flip();
            }
        }
        else
        {
            am.SetBool("movimiento", false);
        }

    }
    public void AtaqueAlJugador()
    {
        // Detectar enemigos en rango de ataque
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, layerPlayer);

        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<CombateJugador>().RecibirDamage(ataque.Valor);
        }
    }

    void Flip()
    {

        mirandoDerecha = !mirandoDerecha;

        // Cambiar escala del enemigo
        enemigoGFX.localScale = new Vector3(mirandoDerecha ? 1f : -1f, 1f, 1f);

        // Actualizar la posición de la hitbox
        FlipHitBox();


    }

    void FlipHitBox()
    {

        // Restauramos la posición inicial de la hitbox en X y la invertimos según la dirección
        hitbox.transform.localPosition = new Vector3(posicionInicialXHitbox * (mirandoDerecha ? 1 : -15),
                                                     hitbox.transform.localPosition.y,
                                                     hitbox.transform.localPosition.z);

        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.offset = new Vector2(Mathf.Abs(circleCollider.offset.x) * (mirandoDerecha ? 1 : -1),
                                                circleCollider.offset.y);
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
