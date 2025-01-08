using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Clase principal que da lógica a las ranuras del inventario de objetos consumibles.
/// </summary>
public class RanuraObjetoConsumible : MonoBehaviour, IPointerClickHandler
{
   
    // Información sobre el objeto.
    public string nombreObjeto;
    public int cantidad;
    public Sprite sprite;
    public string descripcionObjeto;
    public bool estaLleno;
    public Sprite spriteRanuraVacia;
    public TipoObjeto tipoObjeto;

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
    
    /// <summary>
    /// Añade un objeto a la ranura. 
    /// </summary>
    /// <param name="nombreObjeto">Nombre del objeto.</param>
    /// <param name="cantidad">Cantidad del objeto.</param>
    /// <param name="sprite">Sprite del objeto.</param>
    /// <param name="descripcionObjeto">Descripción del objeto.</param>
    /// <param name="tipoObjeto">Tipo del objeto.</param>
    /// <returns>Cantidad sobrante si se excede el límite.</returns>
    public int AddObjeto(string nombreObjeto, int cantidad, Sprite sprite, string descripcionObjeto, TipoObjeto tipoObjeto) {
        if (estaLleno) {
            return cantidad;
        }

        this.tipoObjeto = tipoObjeto;
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
    /// <summary>
    /// Método que da lógica a los clics del jugdor.
    /// </summary>
    /// <param name="eventData">Datos del evento de clic.</param>
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
    /// <summary>
    /// Maneja el clic izquierdo sobre la ranura. Si el objeto está seleccionado, intenta usarlo.
    /// Si no está seleccionado, lo selecciona y actualiza la descripción.
    /// </summary>
    public void OnClickIzquierdo()
    {
        // Si está seleccionado y se vuelve a hacer click se usa el objeto.
        if (estaSeleccionadoObjeto) {
            // Comprobamos si las condiciones permiten usar el objeto. (Ejemplo: No poder usar una poción si el jugador tiene la vida máxima)
            bool objetoUsable = inventarioManager.UsarObjeto(nombreObjeto);
            if (objetoUsable)
            {
                RestarCantidad();
            }
            
        }
        else { 
            inventarioManager.DeseleccionarSlots(); 
            objetoSeleccionado.SetActive(true);
            estaSeleccionadoObjeto = true;
            imagenObjetoDescripcion.sprite = sprite;
            textoNombreObjeto.text = nombreObjeto;
            textoDescripcionObjeto.text = descripcionObjeto;
            if (imagenObjetoDescripcion.sprite == null) {
                imagenObjetoDescripcion.sprite = spriteRanuraVacia;
            }
        }


    }
    /// <summary>
    /// Si no queda más cantidad de un grupo de consumibles, se vacía la ranura eliminando la cantidad y reseteando los elementos visuales.
    /// </summary>
    private void VaciarRanura()
    {
        textoCantidad.enabled = false;
        imagenObjeto.sprite = spriteRanuraVacia;
        textoNombreObjeto.text = "";
        textoDescripcionObjeto.text = "";
        imagenObjetoDescripcion.sprite = spriteRanuraVacia;

    }
    
    /// <summary>
    /// Maneja el clic derecho sobre la ranura. Suelta un objeto en el mundo y reduce la cantidad en la ranura.
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

        // Introducimos collider al objeto tirado
        objetoATirar.AddComponent<BoxCollider2D>();

        // Posición donde se tira el objeto
        objetoATirar.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1,0,0);
        //objetoATirar.transform.localScale = new Vector3(.5f,.5f,.5f);
        RestarCantidad();

    }

    /// <summary>
    /// Reduce la cantidad del objeto en la ranura. Si llega a cero, llama al método de vacíar la ranura.
    /// </summary>
    private void RestarCantidad() {
        this.cantidad -= 1;
        textoCantidad.text = this.cantidad.ToString();
        if (this.cantidad <= 0)
        {
            VaciarRanura();
        }

    }
}
