using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    [SerializeField]
    public string nombreObjeto;

    [SerializeField]
    public int cantidad;

    [SerializeField]
    public Sprite sprite;

    [TextArea]
    [SerializeField]
    public string descripcionObjeto;

    private bool jugadorDentroCollider = false;

    private InventarioManager inventarioManager;

    public TipoObjeto tipoObjeto;

    void Start()
    {
        inventarioManager = GameObject.Find("CanvasInventario").GetComponent<InventarioManager>();

    }

   
    void Update()
    {

        if (jugadorDentroCollider && Input.GetKeyDown(KeyCode.E))
        {
            int objetosSobrantes = inventarioManager.AddObjeto(nombreObjeto, cantidad, sprite, descripcionObjeto, tipoObjeto);
            Debug.Log("Entrando en colision: " + objetosSobrantes);
            if (objetosSobrantes <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                cantidad = objetosSobrantes;
                Debug.Log("Entro else");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            jugadorDentroCollider = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            jugadorDentroCollider = false;
        }
    }

}
