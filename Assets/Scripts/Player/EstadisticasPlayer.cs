using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EstadisticasPlayer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textoNivel, textoAtaque, textoVida, textoArmadura;


    public EstadisticasBase ataque;
    public EstadisticasBase vida;
    public EstadisticasBase mana;
    public EstadisticasBase armadura;
    public EstadisticasBase rangoAtaque;
    public EstadisticasBase velocidadAtaque;
    public EstadisticasBase velocidadMovimiento;
    public int nivelPlayer;
    public int puntosExperienciaActual;
    public int maximoPuntosNivel = 100;

   
    void Awake()
    {
        DatosGuardados datos = GuardadoManager.instancia.CargarDatos();
        if (datos != null)
        {
            
            Debug.Log("Datos cargados: Monedas = " + datos.monedas);
        }
        else
        {
            Debug.LogWarning("No se pudieron cargar los datos. Archivo no encontrado o está corrupto.");
        }

        nivelPlayer = 6;
        puntosExperienciaActual = 0;
        maximoPuntosNivel = 100;

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


    private void Start()
    {

        ActualizarEstadistasEquipamiento();

    }

    public void GanarExperiencia(int puntosExperiencia)
    {
        puntosExperienciaActual += puntosExperiencia;

        // Manejamos el nivel en funcion de los puntos recibidos.
        while (puntosExperienciaActual >= maximoPuntosNivel)
        {
            puntosExperienciaActual -= maximoPuntosNivel;
            SubirNivel();
        }

        ActualizarEstadistasEquipamiento();
    }


    public void SubirNivel() {
        nivelPlayer++;
        maximoPuntosNivel = Mathf.RoundToInt(maximoPuntosNivel * 1.5f); 
        
        Debug.Log($"¡Subiste al nivel {nivelPlayer}!");


        vida.addModificador(new ModificadorEstadisticas(10, TipoModificadorEstadistica.Plano, this));
        ataque.addModificador(new ModificadorEstadisticas(2, TipoModificadorEstadistica.Plano, this));
        armadura.addModificador(new ModificadorEstadisticas(1, TipoModificadorEstadistica.Plano, this));
        

    }

    public void ActualizarEstadistasEquipamiento() {
        textoAtaque.text = ataque.Valor.ToString();
        textoVida.text = vida.Valor.ToString();
        textoArmadura.text = armadura.Valor.ToString();
        textoNivel.text = nivelPlayer.ToString();

    }


}
