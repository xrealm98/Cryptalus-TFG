using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que gestiona las estadísticas del jugador, incluyendo vida, ataque, armadura, y nivel. 
/// Gestiona la experiencia que el nivel de persdonaje.
/// </summary>
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
    GameObject equipamientoCanvas;
    public Slider expSlider;
    public TMP_Text textoNivelSlider;

    /// <summary>
    /// Inicializa las estadísticas del jugador a partir de los datos guardados o valores predeterminados.
    /// </summary>
    void Awake()
    {
        
        DatosGuardados datos = GuardadoManager.instancia.CargarDatos();

        nivelPlayer = 1;
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
        Debug.Log("Estadísticas cargadas: Vida=" + vida.Valor + ", Armadura=" + armadura.Valor + ", Ataque=" + ataque.Valor);
    }


    private void Start()
    {

        equipamientoCanvas = GameObject.Find("PanelEquipamiento");
        textoNivel = GameObject.Find("EstadisticaNivel").GetComponent<TMP_Text>();
        textoAtaque = GameObject.Find("EstadisticaAtaque").GetComponent<TMP_Text>();
        textoVida = GameObject.Find("EstadisticaVida").GetComponent<TMP_Text>();
        textoArmadura = GameObject.Find("EstadisticaArmadura").GetComponent<TMP_Text>();
        
        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        textoNivelSlider = GameObject.Find("TextoNivel").GetComponent<TMP_Text>();
        equipamientoCanvas.SetActive(false);
        ActualizarEstadistasEquipamiento();
        ActualizarSliderExp(); 

    }

    /// <summary>
    /// Incrementa los puntos de experiencia del jugador y maneja la lógica para subir de nivel junto su slider.
    /// </summary>
    /// <param name="puntosExperiencia"> Experiencia obtenida.</param>
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
        ActualizarSliderExp();
    }

    /// <summary>
    /// Incrementa el nivel del jugador y mejora las estadísticas básicas.
    /// </summary>
    public void SubirNivel() {
        nivelPlayer++;
        maximoPuntosNivel = Mathf.RoundToInt(maximoPuntosNivel * 1.5f); 
        
        Debug.Log($"¡Subiste al nivel {nivelPlayer}!");


        vida.addModificador(new ModificadorEstadisticas(10, TipoModificadorEstadistica.Plano, this));
        ataque.addModificador(new ModificadorEstadisticas(2, TipoModificadorEstadistica.Plano, this));
        armadura.addModificador(new ModificadorEstadisticas(1, TipoModificadorEstadistica.Plano, this));
        

    }
    
    /// <summary>
    /// Actualiza las estadísticas mostradas en el inventario de equipamiento.
    /// </summary>
    public void ActualizarEstadistasEquipamiento() {
        textoAtaque.text = ataque.Valor.ToString();
        textoVida.text = vida.Valor.ToString();
        textoArmadura.text = armadura.Valor.ToString();
        textoNivel.text = nivelPlayer.ToString();

    }
    
    /// <summary>
    /// Actualiza el estado de la barra de experiencia.
    /// </summary>
    public void ActualizarSliderExp() {
        expSlider.maxValue = maximoPuntosNivel;
        expSlider.value = puntosExperienciaActual;
        textoNivelSlider.text = "Nivel: " + nivelPlayer;


    }


}
