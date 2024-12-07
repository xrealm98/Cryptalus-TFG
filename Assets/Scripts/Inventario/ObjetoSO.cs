using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objetos/Consumible")]
public class ObjetoSO : ScriptableObject
{
    public string nombreObjeto;
    public Sprite sprite;
    public string descripcion;
    public int cantidad;
    public Vector3 escala = new();
    public TipoObjeto tipoObjeto;
    public EstadisticaACambiar estadisticaACambiar = new EstadisticaACambiar();
    public float cantidadModificadorEstadistica;

    public TipoDeCambio tipoDeCambio = new TipoDeCambio();

    public AtributosACambiar atributosACambiar = new AtributosACambiar();
    public float cantidadModificadorAtributo;


    public bool UsarObjeto() {
        
        CombateJugador combateJugador = GameObject.Find("Player").GetComponent<CombateJugador>();
       
        if (estadisticaACambiar == EstadisticaACambiar.vida) {
           if(combateJugador.vidaActual == combateJugador.vidaMaxima) {
                return false;
           }
           else{
                if (tipoDeCambio == TipoDeCambio.Porcentual)
                {
                    float cantidadCuracion = combateJugador.vidaMaxima * (cantidadModificadorEstadistica / 100f);
                    combateJugador.curarVida(cantidadCuracion);
                }
                else
                {
                    combateJugador.curarVida(cantidadModificadorEstadistica);
                }
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

    public enum TipoDeCambio { 
        Plano,
        Porcentual
    };

    public enum AtributosACambiar
    {
        ninguno,
        ataque,
        armadura
    };

}
