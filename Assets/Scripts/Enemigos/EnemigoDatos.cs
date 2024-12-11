using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoEnemigoStats", menuName = "Enemigos/Stats")]
public class EnemigoDatos : ScriptableObject
{
    public string nombreTipoEnemigo;
    public float vida;
    public float ataque;
    public float armadura;
    public float velocidadMovimiento;
    public float velocidadAtaque;
    public float rangoAtaque;
    public float rangoDetencion;

}
