using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ScriptableObject que representa un objeto consumible en el juego.
/// </summary>
[CreateAssetMenu(menuName = "Objetos/Consumible")]
public class ObjetoConsumibleSO : ScriptableObject
{
    public string nombreObjeto;
    public Sprite sprite;
    public string descripcion;
    public int cantidad;
    public Vector3 escala = new();
    /// <summary>Enum para seleccionar el tipo de objeto.</summary>
    public TipoObjeto tipoObjeto;
    /// <summary>Estad�stica que se modificar� al usar este objeto (ejemplo: vida o man�).</summary>
    public EstadisticaACambiar estadisticaACambiar = new EstadisticaACambiar();
    /// <summary>Cantidad que se modifica.</summary>
    public float cantidadModificadorEstadistica;
    
    /// <summary>Define si el cambio de la estad�stica es plano o porcentual.</summary>
    public TipoDeCambio tipoDeCambio = new TipoDeCambio();

    /// <summary>Atributo que se modificar� al usar este objeto (ejemplo: ataque o armadura).</summary>
    public AtributosACambiar atributosACambiar = new AtributosACambiar();
    public float cantidadModificadorAtributo;
    /// <summary>Cantidad que se modifica del atributo.</summary>

    /// <summary>
    /// Aplica el efecto del objeto consumible al jugador.
    /// </summary>
    /// <returns>Devuelve true si el objeto se usa, y false en caso contrario.</returns>
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
                    combateJugador.CurarVida(cantidadCuracion);
                }
                else
                {
                    combateJugador.CurarVida(cantidadModificadorEstadistica);
                }
                return true;
           }
        }
        return false;
    
    
    }
    
    /// <summary>
    /// Tipos de estad�sticas que pueden ser modificadas.
    /// </summary>
    public enum EstadisticaACambiar { 
        ninguno,
        vida,
        mana
    };
    
    /// <summary>
    /// Define c�mo se aplica el cambio a las estad�sticas: plano  o porcentual.
    /// </summary>
    public enum TipoDeCambio { 
        Plano,
        Porcentual
    };
    
    /// <summary>
    /// Tipos de atributos que pueden ser modificados.
    /// </summary>
    public enum AtributosACambiar
    {
        ninguno,
        ataque,
        armadura
    };

}
