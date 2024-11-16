using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpcionesMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] arrayResoluciones;
    private const string ESCENA_MENU_PRINCIPAL = "Menu Principal";
    public TMP_Dropdown  bttnDropdownResolucion;
    
    public GameObject InterfazPausa;
    public GameObject InterfazOpciones;

    private void Start()
    {
        arrayResoluciones = Screen.resolutions;
        bttnDropdownResolucion.ClearOptions();
        List<string> opciones = new List<string>();
        int indiceResolucionActual = 0;
        for (int i = 0; i < arrayResoluciones.Length; i++) {
            string opcion = arrayResoluciones[i].width + " x " + arrayResoluciones[i].height;
            opciones.Add(opcion);
            if (arrayResoluciones[i].width == Screen.currentResolution.width && arrayResoluciones[i].height == Screen.currentResolution.height) {
                indiceResolucionActual = i;
            }
        }
        bttnDropdownResolucion.AddOptions(opciones);
        bttnDropdownResolucion.value = indiceResolucionActual;
        bttnDropdownResolucion.RefreshShownValue();
    }
    public void establecerResolucion(int iIndiceResolucion) {
        Debug.Log("Entro");
        Resolution resolucion = arrayResoluciones[iIndiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
    public void establecerVolumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
        Debug.Log(volumen);
    }
    public void bttnPantallaCompleta(bool bEstaFull) {
        Debug.Log("Entro");
        Screen.fullScreen = bEstaFull;
    }
    public void establecerCalidad(int indiceCalidad) {
        QualitySettings.SetQualityLevel(indiceCalidad);
    }

    public void VolverEscena()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
      
        // Verificar si estamos en el menú de pausa o en el menú principal
        if (escenaActual == "Opciones")
        {
            SceneManager.LoadScene(ESCENA_MENU_PRINCIPAL);
         
           
        } else {
            Debug.Log("Entro");
            InterfazOpciones.SetActive(false);
            InterfazPausa.SetActive(true); 

        }
    }
}





