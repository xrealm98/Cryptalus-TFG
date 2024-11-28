using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    [SerializeField]
    private string nombreObjeto;

    [SerializeField]
    private int cantidad;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string descripcionObjeto;

    private InventarioManager inventarioManager;
    // Start is called before the first frame update
    void Start()
    {
        inventarioManager = GameObject.Find("CanvasInventario").GetComponent<InventarioManager>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.tag == "Player") {
            int objetosSobrantes = inventarioManager.AddObjeto(nombreObjeto, cantidad, sprite, descripcionObjeto);
            Debug.Log("Entrando en colision: " + objetosSobrantes);
            if (objetosSobrantes <= 0)
            {
                Destroy(gameObject);
            }
            else { 
             cantidad = objetosSobrantes;
                Debug.Log("Entro else");
            }

        }
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
