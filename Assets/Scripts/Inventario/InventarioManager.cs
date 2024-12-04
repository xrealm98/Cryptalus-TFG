using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TipoObjeto
{
    ninguno,
    consumible,
    cabeza,
    cuerpo,
    guantes,
    botas,
    accesorio,
    manos2,
    manoPrincipal,
    manoSecundaria
};

public class InventarioManager : MonoBehaviour
{

    public GameObject MenuInventario;
    public GameObject MenuEquipamiento;

    public RanuraObjeto[] ranuraObjeto;
    public RanuraObjetoEquipamiento[] ranuraObjetoEquipamiento;
    public ObjetoSO[] objetoSOs;
    public RanuraEquipada[] ranuraEquipada;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventario();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Equipamiento();
        }


    }
    void Inventario()
    {

        if (MenuInventario.activeSelf)
        {
            Time.timeScale = 1;
            MenuInventario.SetActive(false);
            MenuEquipamiento.SetActive(false);

        }
        else
        {
            Time.timeScale = 0;
            MenuInventario.SetActive(true);
            MenuEquipamiento.SetActive(false);


        }
    }

    void Equipamiento()
    {

        if (MenuEquipamiento.activeSelf)
        {
            Time.timeScale = 1;
            MenuInventario.SetActive(false);
            MenuEquipamiento.SetActive(false);

        }
        else
        {
            Time.timeScale = 0;
            MenuInventario.SetActive(false);
            MenuEquipamiento.SetActive(true);


        }

    }

    public bool UsarObjeto(string nombreObjeto)
    {
        for (int i = 0; i < objetoSOs.Length; i++)
        {
            if (objetoSOs[i].nombreObjeto == nombreObjeto)
            {
                bool objetoUsable = objetoSOs[i].UsarObjeto();
                return objetoUsable;
            }
        }
        return false;
    }

    public int AddObjeto(string nombreObjeto, int cantidad, Sprite sprite, string descripcionObjeto, TipoObjeto tipoObjeto)
    {
        if (tipoObjeto == TipoObjeto.consumible)
        {
            for (int i = 0; i < ranuraObjeto.Length; i++)
            {
                if (ranuraObjeto[i].estaLleno == false && ranuraObjeto[i].nombreObjeto == nombreObjeto || ranuraObjeto[i].cantidad == 0)
                {
                    int objetosSobrantes = ranuraObjeto[i].AddObjeto(nombreObjeto, cantidad, sprite, descripcionObjeto, tipoObjeto);
                    if (objetosSobrantes > 0)
                    {
                        objetosSobrantes = AddObjeto(nombreObjeto, objetosSobrantes, sprite, descripcionObjeto, tipoObjeto);
                    }
                    return objetosSobrantes;

                }
            }
            return cantidad;
        }
        else
        {
            for (int i = 0; i < ranuraObjetoEquipamiento.Length; i++)
            {
                if (ranuraObjetoEquipamiento[i].estaLleno == false && ranuraObjetoEquipamiento[i].nombreObjeto == nombreObjeto || ranuraObjetoEquipamiento[i].cantidad == 0)
                {
                    int objetosSobrantes = ranuraObjetoEquipamiento[i].AddObjeto(nombreObjeto, cantidad, sprite, descripcionObjeto, tipoObjeto);
                    if (objetosSobrantes > 0)
                    {
                        objetosSobrantes = AddObjeto(nombreObjeto, objetosSobrantes, sprite, descripcionObjeto, tipoObjeto);
                    }
                    return objetosSobrantes;

                }
            }
            return cantidad;



        }


    }
    public void DeseleccionarSlots()
    {
        for (int i = 0; i < ranuraObjeto.Length; i++)
        {
            ranuraObjeto[i].objetoSeleccionado.SetActive(false);
            ranuraObjeto[i].estaSeleccionadoObjeto = false;
        }

        for (int i = 0; i < ranuraObjetoEquipamiento.Length; i++)
        {
            ranuraObjetoEquipamiento[i].objetoSeleccionado.SetActive(false);
            ranuraObjetoEquipamiento[i].estaSeleccionadoObjeto = false;
        }

        for (int i = 0; i < ranuraEquipada.Length; i++)
        {
            ranuraEquipada[i].objetoSeleccionado.SetActive(false);
            ranuraEquipada[i].estaSeleccionadoObjeto = false;
        }
    }

}
