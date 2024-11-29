using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class RanuraObjetoEquipamiento : MonoBehaviour, IPointerClickHandler
{

    // Informaci�n sobre el objeto.
    public string nombreObjeto;
    public int cantidad;
    public Sprite sprite;
    public string descripcionObjeto;
    public bool estaLleno;
    public Sprite spriteRanuraVacia;
    public TipoObjeto tipoObjeto;


    // Informaci�n sobre la ranura de objeto.
    [SerializeField]
    private TMP_Text textoCantidad;

    [SerializeField]
    private Image imagenObjeto;

    public GameObject objetoSeleccionado;
    public bool estaSeleccionadoObjeto;

    private InventarioManager inventarioManager;

    private void Start()
    {
        inventarioManager = GameObject.Find("CanvasInventario").GetComponent<InventarioManager>();
    }

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

        this.descripcionObjeto = descripcionObjeto;

        this.cantidad = 1;
        estaLleno = true;
        
        return 0;
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

    public void OnClickIzquierdo()
    {
        // Si est� seleccionado y se vuelve a hacer click se usa el objeto.
        if (estaSeleccionadoObjeto)
        {
            // Comprobamos si las condiciones permiten usar el objeto. (Ejemplo: No poder usar una poci�n si el jugador tiene la vida m�xima)
            bool objetoUsable = inventarioManager.UsarObjeto(nombreObjeto);
            if (objetoUsable)
            {
                restarCantidad();
            }

        }
        else
        {
            inventarioManager.deseleccionarSlots();
            objetoSeleccionado.SetActive(true);
            estaSeleccionadoObjeto = true;
        
        }


    }

    private void VaciarRanura()
    {
        textoCantidad.enabled = false;
        imagenObjeto.sprite = spriteRanuraVacia;

    }

    public void OnClickDerecho()
    {
        GameObject objetoATirar = new GameObject(nombreObjeto);
        Objeto nuevoObjeto = objetoATirar.AddComponent<Objeto>();
        nuevoObjeto.cantidad = 1;
        nuevoObjeto.nombreObjeto = nombreObjeto;
        nuevoObjeto.sprite = sprite;
        nuevoObjeto.descripcionObjeto = descripcionObjeto;

        SpriteRenderer sr = objetoATirar.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        //sr.sortingOrder = 5;
        //sr.sortingLayerName = "";

        // Introducimos collider al objeto tirado
        objetoATirar.AddComponent<BoxCollider2D>();

        // Posici�n donde se tira el objeto
        objetoATirar.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1, 0, 0);
        //objetoATirar.transform.localScale = new Vector3(.5f,.5f,.5f);
        restarCantidad();

    }
    private void restarCantidad()
    {
        this.cantidad -= 1;
        textoCantidad.text = this.cantidad.ToString();
        if (this.cantidad <= 0)
        {
            VaciarRanura();
        }

    }
}