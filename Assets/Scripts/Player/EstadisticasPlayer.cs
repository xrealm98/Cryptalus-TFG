using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasPlayer : MonoBehaviour
{

    public EstadisticasBase ataque;
    public EstadisticasBase vida;
    public EstadisticasBase mana;
    public EstadisticasBase armadura;
    public EstadisticasBase rangoAtaque;
    public EstadisticasBase velocidadAtaque;
    public EstadisticasBase velocidadMovimiento;

    // Start is called before the first frame update
    void Start()
    {
        ataque = new EstadisticasBase(40);
        vida = new EstadisticasBase(100);
        mana = new EstadisticasBase(40);
        armadura = new EstadisticasBase(12);
        rangoAtaque = new EstadisticasBase(0.75f);
        velocidadAtaque = new EstadisticasBase(2f);
        velocidadMovimiento = new EstadisticasBase(5);
       // ataque.addModificador(new ModificadorEstadisticas(0.2f, TipoModificadorEstadistica.Porcentaje));
       // Debug.Log(ataque.Valor);
    }


}
