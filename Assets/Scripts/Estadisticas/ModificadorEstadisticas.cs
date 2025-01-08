using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoModificadorEstadistica { 
    Plano = 100,
    PorcentajeAditivo = 200,
    PorcentajeMult = 300,
}

public class ModificadorEstadisticas{

    /// <summary> Valor del modificador. </summary>
    public readonly float Valor;
    /// <summary> Tipo de modificador (Plano, PorcentajeAditivo, PorcentajeMult). </summary>
    public readonly TipoModificadorEstadistica Tipo;
    /// <summary> Orden de cálculo para determinar la prioridad del modificador. </summary>
    public readonly int OrdenCalculo;
    /// <summary> La fuente desde donde viene la modificación. </summary>
    public readonly object Fuente;


    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo, int orden, object fuente) { 
    
        Valor = valor;
        Tipo = tipo;
        OrdenCalculo = orden;
        Fuente = fuente;
    }
    // Preparamos varios constructores para acomodarlos de manera que algunos valores no necesiten ser introducidos.

    /// <summary>
    /// Constructor simplificado que asume que el orden de cálculo corresponde al tipo de modificador.
    /// </summary>
    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo) : this(valor, tipo, (int)tipo, null) {  }

    /// <summary>
    /// Constructor que permite especificar un orden pero sin fuente.
    /// </summary>
    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo, int orden) : this(valor, tipo, orden, null) { }

    /// <summary>
    /// Constructor que permite especificar una fuente pero utiliza el tipo como orden de cálculo.
    /// </summary>
    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo, object fuente) : this(valor, tipo, (int)tipo, fuente) { }
}
