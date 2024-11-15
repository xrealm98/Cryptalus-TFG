using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoModificadorEstadistica { 
    Plano = 100,
    PorcentajeAditivo = 200,
    PorcentajeMult = 300,
}

public class ModificadorEstadisticas{

    public readonly float Valor;
    public readonly TipoModificadorEstadistica Tipo;
    public readonly int OrdenCalculo;
    public readonly object Fuente;


    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo, int orden, object fuente) { 
    
        Valor = valor;
        Tipo = tipo;
        OrdenCalculo = orden;
        Fuente = fuente;
    }
    // Preparamos varios constructores para acomodarlos de manera que algunos valores no necesiten ser introducidos.
    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo) : this(valor, tipo, (int)tipo, null) {  }
    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo, int orden) : this(valor, tipo, orden, null) { }
    public ModificadorEstadisticas(float valor, TipoModificadorEstadistica tipo, object fuente) : this(valor, tipo, (int)tipo, fuente) { }
}
