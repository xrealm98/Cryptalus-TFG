using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scriptTienda : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoMonedas;
    void Start()
    {
        ActualizarTextoMonedas(); 
    }

    public void ActualizarTextoMonedas()
    {
        if (textoMonedas != null && MonedasManager.instancia != null)
        {
            textoMonedas.text = "Monedas: " + MonedasManager.instancia.GetMonedasTotal().ToString();
        }
        else
        {
            textoMonedas.text = "Monedas: 0";
            Debug.LogWarning("Texto de monedas no asignado en el Inspector.");
        }
    }

    public void ComprarMejora(int valor) {
        if (MonedasManager.instancia.GetMonedasTotal() >= valor) {
            MonedasManager.instancia.AddMonedas(-valor);
            ActualizarTextoMonedas();
            Debug.Log("Se ha realizado una compra con valor de: "+ valor);

        }
    }
}
