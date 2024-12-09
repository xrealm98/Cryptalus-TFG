using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EscalerasNivel : MonoBehaviour
{
    private bool jugadorDentroCollider = false;


    private void Update()
    {
        if (jugadorDentroCollider && Input.GetKeyDown(KeyCode.E)){

            NivelManager.instancia.CargarNivel();
        
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorDentroCollider = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorDentroCollider = false;
        }
    }


}
