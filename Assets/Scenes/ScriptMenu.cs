using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    public void btContinuarPartida()
    {

    }

    public void btIniciarPartida()
    {
        SceneManager.LoadScene("Partida");
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
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}
