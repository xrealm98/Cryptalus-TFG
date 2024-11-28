using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjetoSO : ScriptableObject
{
    public string nombreObjeto;
    public EstadisticaACambiar estadisticaACambiar = new EstadisticaACambiar();
    public float cantidadModificadorEstadistica;

    public AtributosACambiar atributosACambiar = new AtributosACambiar();
    public float cantidadModificadorAtributo;


    public void UsarObjeto() {
        Debug.Log("Uso objeto");

        if (estadisticaACambiar == EstadisticaACambiar.vida) {
            GameObject.Find("Player").GetComponent<CombateJugador>().curarVida(cantidadModificadorEstadistica);
        
        }
    
    
    }


    public enum EstadisticaACambiar { 
        ninguno,
        vida,
        mana
    };

    public enum AtributosACambiar
    {
        ninguno,
        ataque,
        armadura
    };

}
