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


    public bool UsarObjeto() {
        
        CombateJugador combateJugador = GameObject.Find("Player").GetComponent<CombateJugador>();
        if (estadisticaACambiar == EstadisticaACambiar.vida) {
           if(combateJugador.vidaActual == combateJugador.vidaMaxima) {
                return false;
           }
           else{ 
                combateJugador.curarVida(cantidadModificadorEstadistica);
                return true;
           }
        }
        return false;
    
    
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
