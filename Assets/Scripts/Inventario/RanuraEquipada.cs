using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

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

    public void EquiparPieza(Sprite spriteObjeto, string nombreObjeto, string descripcionObjeto)
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

    private void DesequiparPieza()
    {
        inventarioManager.DeseleccionarSlots();
        inventarioManager.AddObjeto(nombreObjeto, 1, spriteObjeto, descripcionObjeto, tipoObjeto);
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
