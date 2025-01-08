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
    [SerializeField] private float distanciaChequeo = 1f;

    void Start() {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        am = GetComponent<Animator>();
        AjustarRotacionCofre();

    }

    private void Update()
    {
        if (jugadorDentroCollider && Input.GetKeyDown(KeyCode.E) && !cofreYaAbierto)
        {
            FindObjectOfType<AudioManager>().Play("SonidoCofreAbrir");
            AbrirCofre();
            
        }

    }

    private void AbrirCofre() {
        am.SetBool("cofreAbierto", true);
        
      

    }
    private void DropObjeto() {
       
        itemManager.GenerarDrop(transform.position);
        cofreYaAbierto = true;
        Destroy(gameObject, 0.5f);

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
    private void AjustarRotacionCofre()
    {
        // Chequeamos las direcciones (derecha, izquierda, abajo)
        bool hayParedDerecha = Physics2D.Raycast(transform.position, Vector2.right, distanciaChequeo, layerObstaculos);
        bool hayParedIzquierda = Physics2D.Raycast(transform.position, Vector2.left, distanciaChequeo, layerObstaculos);
        bool hayParedAbajo = Physics2D.Raycast(transform.position, Vector2.down, distanciaChequeo, layerObstaculos);

        // Si hay pared a la derecha, el cofre debe mirar a la izquierda
        if (hayParedDerecha)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        // Si hay pared a la izquierda, el cofre debe mirar a la derecha
        else if (hayParedIzquierda)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        // Si hay pared abajo, el cofre debe mirar hacia arriba
        else if (hayParedAbajo)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
