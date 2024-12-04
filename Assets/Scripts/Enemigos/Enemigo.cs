using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Animator am;
    public float vidaMax = 100;
    float vidaActual;
    public BarraVida barraVida;
    public EstadisticasPlayer estadisticasPlayer;

    private float probabilidadDrop = 0.2f;

    private ItemManager itemManager;

    void Start()
    {
        estadisticasPlayer = GameObject.Find("Player").GetComponent<EstadisticasPlayer>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        am = GetComponentInChildren<Animator>();
        vidaActual = vidaMax;
        barraVida.SetVidaMaxima(vidaMax);

    }
    public void RecibirDamage(float damage)
    {
        // daño -= stats.armadura.Valor;
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
       
        vidaActual -= damage;

        barraVida.SetVida(vidaActual);
        am.SetTrigger("recibirGolpe");

        if (vidaActual <= 0)
        {
            MuerteEnemigo();
        }
        
    }
    void MuerteEnemigo() {
        am.SetBool("estaMuerto", true);
        MonedasManager.AddMonedas(300);
        estadisticasPlayer.GanarExperiencia(250);
       
        if (Random.value <= probabilidadDrop)
        {
            itemManager.GenerarDrop(transform.position);
        }
        
        GetComponent<Collider2D>().enabled = false;  
        this.enabled = false;
    }

}
