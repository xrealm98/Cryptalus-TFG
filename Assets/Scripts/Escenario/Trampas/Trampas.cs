using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampas : MonoBehaviour
{
    [SerializeField] private int damage; 
    [SerializeField] private float tiempoEntreActivaciones = 2f;
    Animator am;
    private float tiempoActual = 0f;
    private bool estaDentro = false;

    private void Start()
    {
        am = GetComponent<Animator>();
        if (am == null)
        {
            Debug.LogError($"Animator no encontrado en {gameObject.name}.");
        }
    }
    private void Update()
    {
        // Reducir el contador de tiempo
        if (tiempoActual > 0f)
        {
            tiempoActual -= Time.deltaTime;
        }
  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (am == null)
        {
            return;
        }
        else if (collision.isTrigger) {
            return;
        }
        else if(!estaDentro)
        {
            estaDentro = true;
            am.SetBool("contacto", true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }
        if (!HayPersonasDentro())
        {
            estaDentro = false;
            am.SetBool("contacto", false);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }
        if ((collision.CompareTag("Player") || collision.GetComponent<Enemigo>() != null) && tiempoActual <= 0f){
            am.SetBool("contacto", true);
            tiempoActual = tiempoEntreActivaciones;
        } 

    }


    private void ActivarTrampa()
    {
        Collider2D[] objetosEnRango = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D objeto in objetosEnRango)
        {
            if (objeto.CompareTag("Player"))
            {
                objeto.GetComponent<CombateJugador>().RecibirDamage(damage);
            }
            else{
                Enemigo enemigo = objeto.GetComponent<Enemigo>();
                
                if (enemigo != null)
                {
                    if (objeto.isTrigger)
                    {
                        continue;
                    }
                    enemigo.RecibirDamage(damage);

                }
            }
            am.SetBool("contacto", false);

        }

    }
    private bool HayPersonasDentro()
    {
        Collider2D[] objetosEnRango = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D objeto in objetosEnRango)
        {
            Debug.Log($"Objeto detectado: {objeto.name}");
            if (objeto.CompareTag("Player") || objeto.GetComponent<Enemigo>() != null)
            {
                return true;
            }
        }
        return false;
    }
    }

