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
    }
    private void Update()
    {
        if (estaDentro && tiempoActual <= 0f)
        {
            am.SetBool("contacto", true);
            tiempoActual = tiempoEntreActivaciones; 
        }
        else
        {
            tiempoActual -= Time.deltaTime; 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!estaDentro)
        {
            estaDentro = true;
            am.SetBool("contacto", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!HayPersonasDentro())
        {
            estaDentro = false;
            am.SetBool("contacto", false);
        }

    }
  
    private void ActivarTrampa()
    {
        Collider2D[] objetosEnRango = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D objeto in objetosEnRango)
        {
            if (objeto.CompareTag("Player"))
            {
                objeto.GetComponent<CombateJugador>().recibirDamage(damage);
            }
            else{
                Enemigo enemigo = objeto.GetComponent<Enemigo>();
                
                if (enemigo != null)
                {
                    enemigo.recibirDamage(damage);

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

