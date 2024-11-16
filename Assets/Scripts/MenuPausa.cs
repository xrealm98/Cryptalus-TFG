using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public static bool JuegoPausado = false;
    private const string ESCENA_MENU_PRINCIPAL = "Menu Principal";
    private const string ESCENA_OPCIONES = "Opciones";
    public GameObject InterfazPausa;
  

    void Awake()
    {
        InterfazPausa.SetActive(false);
        Time.timeScale = 1f;
        JuegoPausado = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (JuegoPausado)
            {
                VolverAPartida();
            }
            else {
                pausar();
            }
        }   
    }

    public void VolverAPartida() {
        InterfazPausa.SetActive(false);
        Time.timeScale = 1f;
        JuegoPausado = false;

    }

    public void IrAOpciones()
    {
        //SceneManager.LoadScene(ESCENA_OPCIONES);
    }

    public void GuardarPartida()
    {
    } 

    public void VolverAlMenu()
    {
        Debug.Log("Cargando menu");
        Time.timeScale = 1f; 
        JuegoPausado = false;
        InterfazPausa.SetActive(false);
        SceneManager.LoadScene(ESCENA_MENU_PRINCIPAL);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    void pausar() {
        InterfazPausa.SetActive(true);
        Time.timeScale = 0f;
        JuegoPausado=true;


    }
}
