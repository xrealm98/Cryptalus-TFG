using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjStats", menuName ="Stats")]
public class ObjEstadisticas : ScriptableObject
{
    public EstadisticasBase ataque = new EstadisticasBase(0);
    public EstadisticasBase vida = new EstadisticasBase(0);
    public EstadisticasBase armadura = new EstadisticasBase(0);
    public EstadisticasBase velocidadMovimiento = new EstadisticasBase(0);
}
