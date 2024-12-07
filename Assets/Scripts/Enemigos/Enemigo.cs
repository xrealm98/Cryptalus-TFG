using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Animator am;
    public float vidaMax = 100;
    public float vidaActual;
    private ComportamientoEnemigo comportamientoEnemigo;
    public BarraVida barraVida;
    public EstadisticasPlayer estadisticasPlayer;

    private float probabilidadDrop = 1f;
    private bool estaMuerto = false;

    private ItemManager itemManager;


    void Start()
    {
        estadisticasPlayer = GameObject.Find("Player").GetComponent<EstadisticasPlayer>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        am = GetComponentInChildren<Animator>();
        comportamientoEnemigo = GetComponent<ComportamientoEnemigo>();
        
        vidaActual = comportamientoEnemigo.vida.Valor;
        vidaMax = vidaActual;
        barraVida.SetVidaMaxima(vidaMax);

    }
    public void RecibirDamage(float damage)
    {
        if (estaMuerto) return;
        damage -= comportamientoEnemigo.armadura.Valor;
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        vidaActual -= damage;

        barraVida.SetVida(vidaActual);
        am.SetTrigger("recibirGolpe");

        if (vidaActual <= 0)
        {
            MuerteEnemigo();
        }

    }
    void MuerteEnemigo()
    {
        if (estaMuerto) {
            return;
        }
        estaMuerto = true;
        am.SetBool("estaMuerto", true);
        MonedasManager.AddMonedas(300);
        estadisticasPlayer.GanarExperiencia(250);

        if (Random.value <= probabilidadDrop)
        {
            itemManager.GenerarDrop(transform.position);
        }

        float tiempoDestruccion = am.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, 2);
    }

}
