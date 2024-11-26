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
    void Awake()
    {
        DatosGuardados datos = GuardadoManager.instancia.CargarDatos();

        if (datos != null && datos.ataqueBase !=0)
        {
            ataque = new EstadisticasBase(datos.ataqueBase);
            vida = new EstadisticasBase(datos.vidaBase);
            armadura = new EstadisticasBase(datos.armaduraBase);
        }
        else
        {
            ataque = new EstadisticasBase(40);
            vida = new EstadisticasBase(100);
            armadura = new EstadisticasBase(12);
            
            GuardadoManager.instancia.ActualizarEstadisticas(ataque.Valor, vida.Valor, armadura.Valor);
        }

        

        mana = new EstadisticasBase(40);
        rangoAtaque = new EstadisticasBase(0.75f);
        velocidadAtaque = new EstadisticasBase(2f);
        velocidadMovimiento = new EstadisticasBase(5);
        // ataque.addModificador(new ModificadorEstadisticas(0.2f, TipoModificadorEstadistica.Porcentaje));
        Debug.Log("Estadísticas cargadas: Vida=" + vida.Valor + ", Armadura=" + armadura.Valor + ", Ataque=" + ataque.Valor);
    }


}
