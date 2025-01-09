using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Clase que gestiona los objetos equipados del personaje.
/// Permite equipar y desequipar piezas, y actualiza las estadísticas del personaje.
/// </summary>
public class RanuraEquipada : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image imagenRanura;

    [SerializeField]
    private TMP_Text nombreRanura;

    [SerializeField]
    private TipoObjeto tipoObjeto = new();

    private Sprite spriteObjeto;
    private string nombreObjeto;
    private string descripcionObjeto;
    private Vector3 escala;

    private bool ranuraEnUso;

    [SerializeField]
    public GameObject objetoSeleccionado;

    [SerializeField]
    public bool estaSeleccionadoObjeto;

    [SerializeField]
    private Sprite spriteVacio;

    private InventarioManager inventarioManager;
    private BibliotecaEquipoSO bibliotecaEquipoSO;
    public EstadisticasPlayer estadisticasPlayer;

    private void Start()
    {
        inventarioManager = GameObject.Find("CanvasInventario").GetComponent<InventarioManager>();
        bibliotecaEquipoSO = GameObject.Find("CanvasInventario").GetComponent<BibliotecaEquipoSO>();
        estadisticasPlayer = GameObject.Find("Player").GetComponent<EstadisticasPlayer>();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnClickIzquierdo();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnClickDerecho();
        }
    }

    private void OnClickDerecho()
    {
        DesequiparPieza();

    }

    private void OnClickIzquierdo()
    {
        if (estaSeleccionadoObjeto && ranuraEnUso)
        {
            DesequiparPieza();
        }
        else
        {
            inventarioManager.DeseleccionarSlots();
            objetoSeleccionado.SetActive(true);
            estaSeleccionadoObjeto = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ranuraEnUso)
        {
            TooltipManager.instancia.MostrarTooltip(this.nombreObjeto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        TooltipManager.instancia.OcultarTooltip();
    }
    /// <summary>
    /// Equipa una pieza en la ranura de su tipo.
    /// Introduce el sprite en la ranura y modifica las estadísticas del personaje.
    /// </summary>
    /// <param name="sprite">Sprite del objeto.</param>
    /// <param name="nombreObjeto">Nombre del objeto.</param>
    /// <param name="descripcionObjeto">Descripción del objeto.</param>
    /// <param name="escala"> Tamaño del objeto.</param>
    public void EquiparPieza(Sprite spriteObjeto, string nombreObjeto, string descripcionObjeto, Vector3 escala)
    {

        if (ranuraEnUso)
        {
            DesequiparPieza();
        }
        this.spriteObjeto = spriteObjeto;
        imagenRanura.sprite = this.spriteObjeto;
        nombreRanura.enabled = false;

        this.nombreObjeto = nombreObjeto;
        this.descripcionObjeto = descripcionObjeto;
        this.escala = escala;


        // Actualizar estadisticas del jugador al equipar.
        for (int i = 0; i < bibliotecaEquipoSO.equipamientoSO.Length; i++)
        {
            if (bibliotecaEquipoSO.equipamientoSO[i].nombreObjeto == this.nombreObjeto)
            {
                bibliotecaEquipoSO.equipamientoSO[i].EquiparPieza(estadisticasPlayer);
            }
        }

        ranuraEnUso = true;



    }
    
    /// <summary>
    /// Desequipa la pieza actualmente equipada y la vuelve a añadir en el inventario de equipamiento.
    /// Resta las estadísticas del objeto al perosnaje y se vacía la ranura.
    /// </summary>
    private void DesequiparPieza()
    {
        inventarioManager.DeseleccionarSlots();
        inventarioManager.AddObjeto(nombreObjeto, 1, spriteObjeto, descripcionObjeto, tipoObjeto, escala);
        this.spriteObjeto = spriteVacio;
        imagenRanura.sprite = this.spriteVacio;
        nombreRanura.enabled = true;

        // Actualizar estadisticas del jugador al desequipar el objeto.
        for (int i = 0; i < bibliotecaEquipoSO.equipamientoSO.Length; i++)
        {
            if (bibliotecaEquipoSO.equipamientoSO[i].nombreObjeto == this.nombreObjeto)
            {
                bibliotecaEquipoSO.equipamientoSO[i].DesequiparPieza(estadisticasPlayer);
            }
        }

        ranuraEnUso = false;
        nombreObjeto = string.Empty;
        descripcionObjeto = string.Empty;

    }
}
