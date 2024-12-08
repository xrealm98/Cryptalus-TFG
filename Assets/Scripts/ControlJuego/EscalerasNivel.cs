using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscalerasNivel : MonoBehaviour
{
    private bool jugadorDentroCollider = false;
    public string nombreEscena;

    private void Update()
    {
        if (jugadorDentroCollider && Input.GetKeyDown(KeyCode.E)){
            
            SceneManager.LoadScene(nombreEscena);
        
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
