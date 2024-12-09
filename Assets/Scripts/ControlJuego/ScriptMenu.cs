using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    public void btIniciarPartida()
    {
        NivelManager.instancia.CargarNivel();
    }
    public void btTienda()
    {
        SceneManager.LoadScene("Tienda");
    }
    public void btOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }
    public void btSalirPartida()
    {
        Application.Quit();
    }
}
