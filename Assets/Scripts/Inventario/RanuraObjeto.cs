using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RanuraObjeto : MonoBehaviour, IPointerClickHandler
{
   
    // Información sobre el objeto.
    public string nombreObjeto;
    public int cantidad;
    public Sprite sprite;
    public string descripcionObjeto;
    public bool estaLleno;
    public Sprite spriteRanuraVacia;

    [SerializeField]
    private int numeroMaxObjetos;


    // Información sobre la ranura de objeto.
    [SerializeField]
    private TMP_Text textoCantidad;

    [SerializeField]
    private Image imagenObjeto;

    // Información sobre la descripcion del objeto.
    public Image imagenObjetoDescripcion;
    public TMP_Text textoNombreObjeto;
    public TMP_Text textoDescripcionObjeto;

    public GameObject objetoSeleccionado;
    public bool estaSeleccionadoObjeto;

    private InventarioManager inventarioManager;

    private void Start()
    {
        inventarioManager = GameObject.Find("CanvasInventario").GetComponent<InventarioManager>();
    }

    public int AddObjeto(string nombreObjeto, int cantidad, Sprite sprite, string descripcionObjeto) {
        if (estaLleno) {
            return cantidad;
        }
        this.nombreObjeto = nombreObjeto;
       
        this.sprite = sprite;
        
        imagenObjeto.sprite = sprite;
        
        this.descripcionObjeto = descripcionObjeto;

        this.cantidad += cantidad;
        if (this.cantidad >= numeroMaxObjetos) {
            textoCantidad.text = numeroMaxObjetos.ToString();
            textoCantidad.enabled = true;
            estaLleno = true;

            int objetosExtra = this.cantidad - numeroMaxObjetos;
            this.cantidad = numeroMaxObjetos;
            return objetosExtra;
        }

        textoCantidad.text = this.cantidad.ToString();
        textoCantidad.enabled = true;
        
        return 0;
    }
    public void OnPointerClick(PointerEventData eventData) 
    {
        if (eventData.button == PointerEventData.InputButton.Left) {
            OnClickIzquierdo();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnClickDerecho();
        }

    }

    public void OnClickIzquierdo()
    {
        if (estaSeleccionadoObjeto) {
            inventarioManager.UsarObjeto(nombreObjeto);
        }
        inventarioManager.deseleccionarSlots(); 
        objetoSeleccionado.SetActive(true);
        estaSeleccionadoObjeto = true;
        imagenObjetoDescripcion.sprite = sprite;
        textoNombreObjeto.text = nombreObjeto;
        textoDescripcionObjeto.text = descripcionObjeto;
        if (imagenObjetoDescripcion.sprite == null) {
            imagenObjetoDescripcion.sprite = spriteRanuraVacia;
        }


    }

    public void OnClickDerecho()
    {

    }
}
