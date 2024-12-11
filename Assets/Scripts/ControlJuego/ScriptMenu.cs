using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
   void Start() {
        FindObjectOfType<AudioManager>().Play("TemaPrincipal");
    
    }
    public void btIniciarPartida()
    {
        FindObjectOfType<AudioManager>().StopPlaying("TemaPrincipal");
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
