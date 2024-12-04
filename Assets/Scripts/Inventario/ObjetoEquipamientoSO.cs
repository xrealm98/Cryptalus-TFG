using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objetos/Equipamiento")]
public class ObjetoEquipamientoSO : ScriptableObject
{
    public string nombreObjeto;
    public Sprite sprite;
    public string descripcion;
    public float ataque, vida, armadura;
    public Vector3 tamaño = new();
    public TipoObjeto tipoObjeto;
    
    public int nivelMinimoDrop;
    public int nivelMaximoDrop;

    public void EquiparPieza(EstadisticasPlayer estadisticasPlayer)
    {

        // Creación de los modificadores basados en las estadísticas del jugador.
        ModificadorEstadisticas ataqueMod = new(ataque, TipoModificadorEstadistica.Plano, this);
        ModificadorEstadisticas armaduraMod = new (armadura, TipoModificadorEstadistica.Plano, this);
        ModificadorEstadisticas vidaMod = new (vida, TipoModificadorEstadistica.Plano, this);

        // Agregamos los modificadores a las estadísticas
        estadisticasPlayer.ataque.addModificador(ataqueMod);
        estadisticasPlayer.armadura.addModificador(armaduraMod);
        estadisticasPlayer.vida.addModificador(vidaMod);

        estadisticasPlayer.ActualizarEstadistasEquipamiento();

    }

    public void DesequiparPieza(EstadisticasPlayer estadisticasPlayer)      
    {
        Debug.Log("Desequipando pieza: " + this.nombreObjeto);

        // Eliminamos las estadisticas de la fuente.
        estadisticasPlayer.ataque.borrarTodosModificadoresFuente(this);
        estadisticasPlayer.vida.borrarTodosModificadoresFuente(this);
        estadisticasPlayer.armadura.borrarTodosModificadoresFuente(this);

        estadisticasPlayer.ActualizarEstadistasEquipamiento();


    }

}
