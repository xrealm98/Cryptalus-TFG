using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Clase que gestiona las ranuras del inventario de equipamiento.
/// Permite añadir, seleccionar, equipar y desequipar objetos.
/// </summary>
public class RanuraObjetoEquipamiento : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    // Información sobre el objeto.
    public string nombreObjeto;
    public int cantidad;
    public Sprite sprite;
    public string descripcionObjeto;
    public bool estaLleno;
    public Sprite spriteRanuraVacia;
    public TipoObjeto tipoObjeto;

    // Información sobre la ranura de objeto.
    [SerializeField]
    private Image imagenObjeto;

    private BibliotecaEquipoSO bibliotecaEquipoSO;
    // Ranuras de equipamiento
    [SerializeField]
    private RanuraEquipada ranuraCabeza, ranuraCuerpo, ranuraGuantes, ranuraBotas, ranuraAccesorio, ranuraManoPrincipal, ranuraManoSecundaria;

    public GameObject objetoSeleccionado;
    public bool estaSeleccionadoObjeto;

    private InventarioManager inventarioManager;


    private void Start()
    {
        inventarioManager = GameObject.Find("CanvasInventario").GetComponent<InventarioManager>();
        bibliotecaEquipoSO = GameObject.Find("CanvasInventario").GetComponent<BibliotecaEquipoSO>();
    }

    /// <summary>
    /// Añade un objeto a la ranura, actualizando su información y estado.
    /// </summary>
    /// <param name="nombreObjeto">Nombre del objeto.</param>
    /// <param name="cantidad">Cantidad del objeto.</param>
    /// <param name="sprite">Sprite del objeto.</param>
    /// <param name="descripcionObjeto">Descripción del objeto.</param>
    /// <param name="tipoObjeto">Tipo del objeto.</param>
    /// <returns>Cantidad sobrante si la ranura ya estaba llena.</returns>
    public int AddObjeto(string nombreObjeto, int cantidad, Sprite sprite, string descripcionObjeto, TipoObjeto tipoObjeto)
    {
        if (estaLleno)
        {
            return cantidad;
        }

        this.tipoObjeto = tipoObjeto;
        this.nombreObjeto = nombreObjeto;

        this.sprite = sprite;

        imagenObjeto.sprite = sprite;
        imagenObjeto.raycastTarget = false;

        this.descripcionObjeto = descripcionObjeto;

        this.cantidad = 1;
        estaLleno = true;

        return 0;
    }
    
    /// <summary>
    /// Muestra un tooltip con la información del objeto al pasar el ratón sobre la ranura.
    /// </summary>
    /// <param name="eventData">Datos del evento de puntero.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (estaLleno) // Verifica si la ranura está en uso.
        {
            TooltipManager.instancia.MostrarTooltip(this.nombreObjeto);

        }
    }
    
    /// <summary>
    /// Oculta el tooltip al salir del área de la ranura.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {

        TooltipManager.instancia.OcultarTooltip();


    }
    /// <summary>
    /// Gestiona los clics del jugador.
    /// </summary>
    /// <param name="eventData">Datos del evento de clic.</param>
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
    
    /// <summary>
    /// Maneja el clic izquierdo, seleccionando o equipando el objeto.
    /// </summary>
    public void OnClickIzquierdo()
    {
        // Si está seleccionado y se vuelve a hacer click se usa el objeto.
        if (estaSeleccionadoObjeto)
        {
            EquiparPieza();

        }
        else
        {
            inventarioManager.DeseleccionarSlots();
            objetoSeleccionado.SetActive(true);
            estaSeleccionadoObjeto = true;

        }


    }
    /// <summary>
    /// Equipa el objeto en la ranura correspondiente del personaje según su tipo.
    /// </summary>
    private void EquiparPieza()
    {
        if (tipoObjeto == TipoObjeto.cabeza)
        {
            ranuraCabeza.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        if (tipoObjeto == TipoObjeto.cuerpo)
        {
            ranuraCuerpo.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        if (tipoObjeto == TipoObjeto.guantes)
        {
            ranuraGuantes.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        if (tipoObjeto == TipoObjeto.botas)
        {
            ranuraBotas.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        if (tipoObjeto == TipoObjeto.accesorio)
        {
            ranuraAccesorio.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        if (tipoObjeto == TipoObjeto.manoPrincipal)
        {
            ranuraManoPrincipal.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        if (tipoObjeto == TipoObjeto.manoSecundaria)
        {
            ranuraManoSecundaria.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        if (tipoObjeto == TipoObjeto.manos2)
        {
            ranuraManoPrincipal.EquiparPieza(sprite, nombreObjeto, descripcionObjeto);
        }
        VaciarRanura();
    }


    /// <summary>
    /// Vacía la ranura, restaurando su imagen y estado.
    /// </summary>
    private void VaciarRanura()
    {

        imagenObjeto.sprite = spriteRanuraVacia;
        imagenObjeto.raycastTarget = true;
        estaLleno = false;

    }

    /// <summary>
    /// Maneja el clic derecho, quitando el objeto del inventario y creandolo en el nivel.
    /// </summary>
    public void OnClickDerecho()
    {
        GameObject objetoATirar = new GameObject(nombreObjeto);
        objetoATirar.layer = LayerMask.NameToLayer("Objetos");
        Objeto nuevoObjeto = objetoATirar.AddComponent<Objeto>();
        nuevoObjeto.cantidad = 1;
        nuevoObjeto.nombreObjeto = nombreObjeto;
        nuevoObjeto.sprite = sprite;
        nuevoObjeto.descripcionObjeto = descripcionObjeto;
        nuevoObjeto.tipoObjeto = tipoObjeto;

        SpriteRenderer sr = objetoATirar.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        //sr.sortingOrder = 5;
        //sr.sortingLayerName = "";

        // Introducimos collider al objeto tirado
        objetoATirar.AddComponent<BoxCollider2D>();

        // Posición donde se tira el objeto
        objetoATirar.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1, 0, 0);
        objetoATirar.transform.localScale = new Vector3(5,5,5);
        RestarCantidad();

    }

    /// <summary>
    /// Reduce la cantidad del objeto en la ranura y la vacía si no quedan unidades.
    /// </summary>
    private void RestarCantidad()
    {
        this.cantidad -= 1;
        if (this.cantidad <= 0)
        {
            VaciarRanura();
        }

    }

}
