using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDelJuego : MonoBehaviour
{
    private const string ESCENA_MENU_PRINCIPAL = "Menu Principal";

    public void InicializarPlayer() {
        gameObject.SetActive(false);
    }

    public void IniciarPantalla() { 
        gameObject.SetActive(true);
    }

    public void IrMenuPincipal() {
        GuardadoManager.instancia.GuardarDatos();
        LimpiarObjetosPersistentes();
        SceneManager.LoadScene(ESCENA_MENU_PRINCIPAL);
    
    }
    public void SalirJuego() {
        GuardadoManager.instancia.GuardarDatos();
        Application.Quit();
    
    }

    public void LimpiarObjetosPersistentes()
    {
        GameObject[] todosLosObjetos = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in todosLosObjetos)
        {
            if (obj.scene.name == "DontDestroyOnLoad")
            {
                Destroy(obj);
                Debug.Log("Objeto destruido: " + obj.name);
            }
        }
    }

}
