using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreScript : MonoBehaviour
{
    private bool jugadorDentroCollider = false;
    private ItemManager itemManager;
    private Animator am;
    private bool cofreYaAbierto = false;

    [SerializeField] private LayerMask layerObstaculos;

    void Start() {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        am = GetComponent<Animator>();

    }

    private void Update()
    {
        if (jugadorDentroCollider && Input.GetKeyDown(KeyCode.E) && !cofreYaAbierto)
        {
            AbrirCofre();
            
        }

    }

    private void AbrirCofre() {
        am.SetBool("cofreAbierto", true);
        
      

    }
    private void DropObjeto() {
        Vector3 posicionDrop = CalcularPosicionDrop();
        itemManager.GenerarDrop(posicionDrop);
        cofreYaAbierto = true;
        // Destroy(gameObject, 2);

    }

    private Vector3 CalcularPosicionDrop()
    {
       
        float distanciaCheck = 1f;

        // Posiciones relativas que se quieren verificar.
        Vector3 posicionArriba = transform.position + Vector3.up * distanciaCheck;
        Vector3 posicionAbajo = transform.position + Vector3.down * distanciaCheck;

        // Chequeamos si hay obstáculos (posición, tamaño objeto y el layer)
        bool obstaculoArriba = Physics2D.OverlapCircle(posicionArriba, 0.3f, layerObstaculos);
        bool obstaculoAbajo = Physics2D.OverlapCircle(posicionAbajo, 0.3f, layerObstaculos);

        // Miramos la posición
        if (!obstaculoAbajo)
        {
            // El Drop es hacia abajo
            return posicionAbajo; 
        }
        else if (!obstaculoArriba)
        {
            // El Drop es hacia arriba
            return posicionArriba; 
        }
        else
        {
            // Drop encima del cofre si no hay posiciones posibles.
            return transform.position; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorDentroCollider = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorDentroCollider = false;
        }
    }
}
