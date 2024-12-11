using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    private Vector2 movimiento;
    private Rigidbody2D rb;
    private Animator am;
    SpriteRenderer sr;
    private GameObject hitbox;
    private bool mirandoDerecha = true;


    private bool alternarPaso = true;

    public EstadisticasPlayer stats;
    private AudioManager audioManager;
    
    private float pasoTimer = 0f;
    private float tiempoEntrePasos = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        stats = GetComponent<EstadisticasPlayer>();
        hitbox = transform.Find("HitboxGolpe").gameObject;
        audioManager = FindObjectOfType<AudioManager>();

    }
    private void OnMovimiento(InputValue value)
    {
        movimiento = value.Get<Vector2>().normalized;

        // Verificar dirección para orientar el rango de ataque y sprite.
        if (movimiento.x != 0)
        {
            // Cambiar la orientación de la hitbox
            flipHitBox(movimiento.x);
            // Flip del sprite
            flipSprite(movimiento.x);
        }

    }
    void Update()
    {
        rb.AddForce(Vector2.zero);
    }

    public void FixedUpdate()
    {
        // Movimiento y velocidad.
        rb.MovePosition(rb.position + movimiento * stats.velocidadMovimiento.Valor * Time.fixedDeltaTime);
        am.SetBool("movimiento", movimiento.magnitude > 0);



        if (movimiento.magnitude > 0)
        {
            pasoTimer -= Time.fixedDeltaTime;
            if (pasoTimer <= 0)
            {
                string paso = alternarPaso ? "PasosPlayer" : "PasosPlayer2";
                audioManager.Play(paso);
                alternarPaso = !alternarPaso;
                pasoTimer = tiempoEntrePasos;
            }
        }
        else
        {
            audioManager.StopPlaying("PasosPlayer");
            audioManager.StopPlaying("PasosPlayer2");
            pasoTimer = 0f;

        }
    }

    public void flipHitBox(float direccionX)
    {

        if ((direccionX > 0 && !mirandoDerecha) || (direccionX < 0 && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            // Guardamos la posición de la hitbox
            Vector3 posicionHitbox = hitbox.transform.localPosition;
            // Cambiar la posición en X de la hitbox
            posicionHitbox.x *= -1;
            hitbox.transform.localPosition = posicionHitbox;
        }

    }
    private void flipSprite(float direccionX)
    {
        sr.flipX = direccionX < 0;
    }

}
