using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour
{

    public GameObject MenuInventario;
    private bool menuActivo;
    public RanuraObjeto[] ranuraObjeto;
    public ObjetoSO[] objetoSOs;
    

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !menuActivo)
        {
            Time.timeScale = 0;
            MenuInventario.SetActive(true);
            menuActivo = true;
        } else if(Input.GetKeyDown(KeyCode.I) && menuActivo) {
            Time.timeScale = 1;
            MenuInventario.SetActive(false);
            menuActivo = false;
        }
        
    }

    public void UsarObjeto(string nombreObjeto)
    {
        for (int i = 0; i < objetoSOs.Length; i++)
        {
            if (objetoSOs[i].nombreObjeto == nombreObjeto)
            {
                objetoSOs[i].UsarObjeto();
            }

        }
    }

    public int AddObjeto(string nombreObjeto, int cantidad, Sprite sprite, string descripcionObjeto) {

        for (int i = 0; i < ranuraObjeto.Length; i++) {
            if (ranuraObjeto[i].estaLleno == false && ranuraObjeto[i].nombreObjeto == nombreObjeto || ranuraObjeto[i].cantidad == 0) {
                Debug.Log("Entro for inventario manager");
                int objetosSobrantes = ranuraObjeto[i].AddObjeto(nombreObjeto, cantidad, sprite, descripcionObjeto);
                if (objetosSobrantes > 0) {
                    objetosSobrantes = AddObjeto(nombreObjeto, objetosSobrantes, sprite, descripcionObjeto);
                }
                return objetosSobrantes;

            }
        }
        return cantidad;


    }
    public void deseleccionarSlots() {
        for (int i = 0; i < ranuraObjeto.Length; i++) {
            ranuraObjeto[i].objetoSeleccionado.SetActive(false);
            ranuraObjeto[i].estaSeleccionadoObjeto = false;
        }
    }
}
