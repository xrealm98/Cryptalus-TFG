using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public static bool JuegoPausado = false;
    private const string ESCENA_MENU_PRINCIPAL = "Menu Principal";

    public GameObject InterfazPausa;
    public GameObject InterfazOpciones;


    void Awake()
    {
        InterfazPausa.SetActive(false);
        InterfazOpciones.SetActive(false);
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
        InterfazOpciones.SetActive(false);
        Time.timeScale = 1f;
        JuegoPausado = false;

    }

    public void IrAOpciones()
    {
        InterfazOpciones.SetActive(true);
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
        InterfazOpciones.SetActive(false);
        Time.timeScale = 0f;
        JuegoPausado=true;


    }
}
