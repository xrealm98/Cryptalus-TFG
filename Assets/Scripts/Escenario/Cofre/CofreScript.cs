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
}
