using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OpcionesMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] arrayResoluciones;

    public Dropdown bttnDropownResolucion;

    private void Start()
    {
        arrayResoluciones = Screen.resolutions;
        bttnDropownResolucion.ClearOptions();
        List<string> opciones = new List<string>();
        int indiceResolucionActual = 0;
        for (int i = 0; i < arrayResoluciones.Length; i++) {
            string opcion = arrayResoluciones[i].width + " x " + arrayResoluciones[i].height;
            opciones.Add(opcion);
            if (arrayResoluciones[i].width == Screen.currentResolution.width && arrayResoluciones[i].height == Screen.currentResolution.height) {
                indiceResolucionActual = i;
            }
        }
        bttnDropownResolucion.AddOptions(opciones);
        bttnDropownResolucion.value = indiceResolucionActual;
        bttnDropownResolucion.RefreshShownValue();
    }
    public void establecerResolucion(int iIndiceResolucion) {
        Resolution resolucion = arrayResoluciones[iIndiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
    public void establecerVolumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
        Debug.Log(volumen);
    }
    public void bttnPantallaCompleta(bool bEstaFull) { 
        Screen.fullScreen = bEstaFull;
    }
    public void establecerCalidad(int indiceCalidad) {
        QualitySettings.SetQualityLevel(indiceCalidad);
    }
    
}
