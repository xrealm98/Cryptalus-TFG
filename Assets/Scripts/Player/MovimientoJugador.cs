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


    public EstadisticasPlayer stats;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        stats = GetComponent<EstadisticasPlayer>();
        hitbox = transform.Find("HitboxGolpe").gameObject;

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

    public void FixedUpdate() {
        // Movimiento y velocidad.
        rb.MovePosition(rb.position + movimiento * stats.velocidadMovimiento.Valor * Time.fixedDeltaTime);
        am.SetBool("movimiento", movimiento.magnitude > 0);
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
