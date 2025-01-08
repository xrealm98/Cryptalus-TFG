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
    [SerializeField] Slider sliderVolumen;

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

        if (!PlayerPrefs.HasKey("volumen"))
        {
            PlayerPrefs.SetFloat("volumen", 1);
            CargarVolumen();
        }
        else {
            CargarVolumen();
        }
    }
    public void establecerResolucion(int iIndiceResolucion) {
        Resolution resolucion = arrayResoluciones[iIndiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void CargarVolumen() {

        sliderVolumen.value = PlayerPrefs.GetFloat("volumen", sliderVolumen.value);

    }

    public void GuardarVolumen( )
    {
        PlayerPrefs.SetFloat("volumen", sliderVolumen.value);

    }
    public void EstablecerVolumen(float volumen)
    {
        AudioListener.volume = sliderVolumen.value;
        //audioMixer.SetFloat("volumen", volumen);
        GuardarVolumen();
        Debug.Log($"Volumen establecido a: {volumen}");
    }
    public void bttnPantallaCompleta(bool bEstaFull) {
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
            CargarVolumen();

        }
    }
}





