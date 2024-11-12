using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ComportamientoEnemigo : MonoBehaviour
{

    public Transform enemigoGFX;

    public Transform objetivo;
    public Transform puntoAtaque;
    private GameObject hitbox;
    Animator am;
    public LayerMask layerPlayer;

    public float velocidadEnemigo = 600;
    public int dañoAtaque = 40;
    public float velocidadAtaque = 2f;
    float tiempoProximoAtaque = 3f;
    public float rangoAtaque = 1.25f;
   
    
    private bool mirandoDerecha = true;
    public float distanciaProximoPunto = 1.5f;
    Path path;
    int puntoActual;
    bool puntoAlcanzado = false;
    Seeker seeker;
    Rigidbody2D rb;

    private float posicionInicialXHitbox;

    void Start()
    {
        am = GetComponentInChildren<Animator>();
        objetivo = GameObject.FindGameObjectWithTag("Player").transform;
        hitbox = transform.Find("HitboxGolpe").gameObject;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        posicionInicialXHitbox = hitbox.transform.localPosition.x;

        InvokeRepeating("actualizarCamino", 0f, .5f);
        

    }

    void actualizarCamino()
    {
       if (seeker.IsDone()) { 
        seeker.StartPath(rb.position, objetivo.position, caminoCompletado);
        }
    }

    void caminoCompletado(Path p) {
        if (!p.error) {
            path = p;
        }
    
    }


    void FixedUpdate()
    {
        if (am.GetBool("estaMuerto"))
        {
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
           
        }

        if (path == null) {
            return;
        }
        if (puntoActual >= path.vectorPath.Count)
        {
            am.SetBool("movimiento", false);
            if (Time.time >= tiempoProximoAtaque)
            {
                ataque();
                tiempoProximoAtaque = Time.time + 3f / velocidadAtaque;
            }
            puntoAlcanzado = true;
            return;
        }
        else {
            puntoAlcanzado = false;
        }

        Vector2 direccion = ((Vector2)path.vectorPath[puntoActual] - rb.position).normalized;

        Vector2 fuerza = direccion * velocidadEnemigo * Time.deltaTime;

        rb.AddForce(fuerza);

        float distancia = Vector2.Distance(rb.position, path.vectorPath[puntoActual]);

        if (distancia < distanciaProximoPunto) {
            puntoActual++;
        
        }

        comprobarMovimiento(fuerza);
         
    }

    void comprobarMovimiento(Vector2 fuerza) {
        if (fuerza.x >= 0.01f)
        {
            flipHitBox(fuerza.x);
            enemigoGFX.localScale = new Vector3(1f, 1f, 1f);

        }
        else if (fuerza.x <= -0.01f)
        {
            flipHitBox(fuerza.x);
            enemigoGFX.localScale = new Vector3(-1f, 1f, 1f);

        }
        if (fuerza.magnitude >= 0.01f)
        {
            am.SetBool("movimiento", true);
        }
        else
        {
            am.SetBool("movimiento", false);
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

    public void flipHitBox(float direccionX)
    {

        if ((direccionX > 0 && !mirandoDerecha) || (direccionX < 0 && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;

            // Restauramos la posición inicial de la hitbox en X y la invertimos según la dirección
            hitbox.transform.localPosition = new Vector3(posicionInicialXHitbox * (mirandoDerecha ? 1 : -15),
                                                         hitbox.transform.localPosition.y,
                                                         hitbox.transform.localPosition.z);
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
