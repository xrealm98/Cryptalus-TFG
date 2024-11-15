using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;

[Serializable]
public class EstadisticasBase
{
    public float ValorBase;

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
    
    protected bool estaMal = true;
    protected float _value;
    protected float ultimoValorBase = float.MinValue;

    protected readonly List<ModificadorEstadisticas> modificadorEstadisticas;

    // Lista publica que prohibe cambios. Solo para mostrar.
    public readonly ReadOnlyCollection<ModificadorEstadisticas> ModificadorEstadisticas;

    public EstadisticasBase()
    {
        modificadorEstadisticas = new List<ModificadorEstadisticas>();
        ModificadorEstadisticas = modificadorEstadisticas.AsReadOnly();
    }

    public EstadisticasBase(float valorBase) : this() {

        ValorBase = valorBase;
        
     }

    // Introducir un modificador.
    public virtual void addModificador(ModificadorEstadisticas mod) {
        estaMal = true;
        modificadorEstadisticas.Add(mod);
        modificadorEstadisticas.Sort(compararOrdenModificador);
    }

    // Compara el orden para saber como se debe calcular el modificador.

    protected virtual int compararOrdenModificador(ModificadorEstadisticas a, ModificadorEstadisticas b) {
        
        if (a.OrdenCalculo < b.OrdenCalculo) { 
            return -1;
        }
        else if (a.OrdenCalculo > b.OrdenCalculo)
        {
            return 1;
        }
        return 0;   // if (a.OrdenCalculo == b.OrdenCalculo)


    }
    // Eliminar un modificador
    public virtual bool removeModificador(ModificadorEstadisticas mod)
    {
        estaMal = true;
        if (modificadorEstadisticas.Remove(mod)) {
            estaMal = true;
            return true;
        }
        return false;

    }
    // Método para eliminar los modificadores de una sola fuente. Se elimina en reverso para ser más eficiente.
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

    // Metodo que calcula las sumas y multiplicaciones de las estadisticas.
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

    
