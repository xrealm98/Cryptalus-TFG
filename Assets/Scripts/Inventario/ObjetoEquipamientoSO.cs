using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objetos/Equipamiento")]
public class ObjetoEquipamientoSO : ScriptableObject
{
    public string nombreObjeto;
    public float ataque, vida, armadura;
    public void EquiparPieza(EstadisticasPlayer estadisticasPlayer)
    {

        // Creación de los modificadores basados en las estadísticas del jugador.
        ModificadorEstadisticas ataqueMod = new ModificadorEstadisticas(ataque, TipoModificadorEstadistica.Plano, this);
        ModificadorEstadisticas armaduraMod = new ModificadorEstadisticas(armadura, TipoModificadorEstadistica.Plano, this);
        ModificadorEstadisticas vidaMod = new ModificadorEstadisticas(vida, TipoModificadorEstadistica.Plano, this);

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
