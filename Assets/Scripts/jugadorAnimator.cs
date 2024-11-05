using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorAnimator : MonoBehaviour
{
    // Referencias al animator, script del movimiento y al sprite renderer.
    Animator am;
    MovimientoJugador mj;
    SpriteRenderer sr;
   
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        mj = GetComponent<MovimientoJugador>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   // Se mira si el jugador esta en movimiento. Si da 0 significa que no se esta moviendo.
        if (mj.direccionMovimiento.x != 0 || mj.direccionMovimiento.y != 0)
        {
            am.SetBool("movimiento", true);
            fijarDireccionSprite();
        }
        else
        {
            am.SetBool("movimiento", false);
        }
    }
    void fijarDireccionSprite() {
        // Si es menos de 0, significa que va hacia la izquierda.
        if (mj.ultimoVectorHorizontal < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
