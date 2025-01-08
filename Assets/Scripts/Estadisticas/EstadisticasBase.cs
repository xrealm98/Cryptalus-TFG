using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;

/// <summary>
/// La clase trabaja con las estadísticas base. Pueden ser modificadas por diferentes factores.
/// Se permite la acumulación y gestión de modificadores planos, porcentuales y multiplicativos.
/// </summary>
[Serializable]
public class EstadisticasBase
{
    public float ValorBase;
    
    /// <summary>
    /// Valor final calculado de la estadística, teniendo en cuenta todos los modificadores.
    /// Solo se recalcula el valor si el estado ha cambiado.
    /// </summary>
    public float Valor { 
        get {
            if (estaMal || ValorBase != ultimoValorBase) {
                ultimoValorBase = ValorBase;
                _value = calcularValorFinal();
                estaMal = false;
            }
            return _value;
        } 
    }
    /// <summary> Boolean que indica si el valor debe recalcularse debido a cambios en los modificadores. </summary>
    protected bool estaMal = true;

    protected float _value;
    
    // <summary> Último valor base usado para calcular el valor final. </summary>
    protected float ultimoValorBase = float.MinValue;

    protected readonly List<ModificadorEstadisticas> modificadorEstadisticas;

   
    public readonly ReadOnlyCollection<ModificadorEstadisticas> ModificadorEstadisticas;

    public EstadisticasBase()
    {
        modificadorEstadisticas = new List<ModificadorEstadisticas>();
        ModificadorEstadisticas = modificadorEstadisticas.AsReadOnly();
    }

    public EstadisticasBase(float valorBase) : this() {

        ValorBase = valorBase;
        
     }

    /// <summary>
    /// Añade un modificador a la estadística y recalcula el orden de los modificadores.
    /// </summary>
    /// <param name="mod">Modificador a añadir.</param>
    public virtual void addModificador(ModificadorEstadisticas mod) {
        estaMal = true;
        modificadorEstadisticas.Add(mod);
        modificadorEstadisticas.Sort(compararOrdenModificador);
    }

    /// <summary>
    /// Compara dos modificadores para saber el orden del cálculo.
    /// </summary>
    /// <param name="a">Primer modificador.</param>
    /// <param name="b">Segundo modificador.</param>
    /// <returns>-1 si 'a' tiene mayor prioridad, 1 si 'b' tiene mayor prioridad, 0 si son iguales.</returns>
    protected virtual int compararOrdenModificador(ModificadorEstadisticas a, ModificadorEstadisticas b) {
        
        if (a.OrdenCalculo < b.OrdenCalculo) { 
            return -1;
        }
        else if (a.OrdenCalculo > b.OrdenCalculo)
        {
            return 1;
        }
        return 0;   


    }
    /// <summary>
    /// Elimina un modificador específico de la estadística.
    /// </summary>
    /// <param name="mod">Modificador a eliminar.</param>
    /// <returns>True si el modificador se elimina, false en caso contrario.</returns>
    public virtual bool removeModificador(ModificadorEstadisticas mod)
    {
        estaMal = true;
        if (modificadorEstadisticas.Remove(mod)) {
            estaMal = true;
            return true;
        }
        return false;

    }
 
    /// <summary>
    /// Elimina todos los modificadores de una fuente específica. Se elimina en reverso para ser más eficiente.
    /// </summary>
    /// <param name="fuente">Fuente de los modificadores a eliminar.</param>
    /// <returns>True si se eliminaron modificadores, false en caso contrario.</returns>
    public virtual bool borrarTodosModificadoresFuente(object fuente) {
        bool seBorra = false;
        for (int i = modificadorEstadisticas.Count -1; i >= 0; i--) {
            if (modificadorEstadisticas[i].Fuente == fuente) {
                estaMal = true;
                seBorra = true;
                modificadorEstadisticas.RemoveAt(i);

            }
        
        }
        return seBorra; 
    }

    /// <summary>
    /// Calcula el valor final de la estadística aplicando todos los modificadores en orden.
    /// </summary>
    /// <returns>Valor final redondeado a 4 decimales.</returns>
    protected virtual float calcularValorFinal()
    {
        float valorFinal = ValorBase;
        float sumPorcentajeAditivo = 0;
        for (int i = 0; i < modificadorEstadisticas.Count; i++)
        {
            ModificadorEstadisticas mod = modificadorEstadisticas[i];
            if (mod.Tipo == TipoModificadorEstadistica.Plano)
            {
                valorFinal += modificadorEstadisticas[i].Valor;
            }
            else if (mod.Tipo == TipoModificadorEstadistica.PorcentajeAditivo)
            {
                // Vamos iterando y añadiendo todos los modificadores del mismo tipo hasta que encuentra uno diferente o de otro tipo.
               
                if (i + 1 >= modificadorEstadisticas.Count || modificadorEstadisticas[i + 1].Tipo != TipoModificadorEstadistica.PorcentajeAditivo)
                {
                    // Añadimos todos los porcentajes aditivos al valor final.
                    valorFinal *= 1 + sumPorcentajeAditivo;
                    sumPorcentajeAditivo = 0;
                }
            }
            else if (mod.Tipo == TipoModificadorEstadistica.PorcentajeMult) {
                valorFinal *= 1 + mod.Valor;
            
            }
             
        
        }

        return (float)Math.Round(valorFinal, 4);
    }

}

    
