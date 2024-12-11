using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelManager : MonoBehaviour
{
    public static NivelManager instancia;
    public string[] niveles;
    
    // Elementos a cargar en la escena
    public GameObject grid;
    public GameObject playerPrefab;
    public GameObject canvasVidaPrefab;
    public GameObject canvasInventarioPrefab;
    public GameObject canvasFinPartidaPrefab;

    // Copias para instanciar los objetos y no trabajar sobre los Prefab
    private GameObject player;
    private GameObject canvasVida;
    private GameObject canvasInventario;
    private GameObject canvasFinPartida;

    private bool partidaIniciada = false;

    // Mantenemos la presistencia de este script a través de todo el proceso de juego.
    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            instancia = this;
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    // Cuando se llama buscara una escena aleatoria dentro del array y se cargará. Llama a CargarPersonaYInterfaz()
    public void CargarNivel()
    {
        int nivelSeleccionado = Random.Range(0, niveles.Length);
        string nivel = niveles[nivelSeleccionado];
        SceneManager.LoadScene(nivel);
        
        if (!partidaIniciada)
        {
            Debug.Log("Iniciando partida...");
            CargarPersonaYInterfaz();
            partidaIniciada = true;
        }



    }

    void CargarPersonaYInterfaz() {
        player = Instantiate(playerPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        player.name = playerPrefab.name;

        canvasVida = Instantiate(canvasVidaPrefab);
        canvasVida.name = canvasVidaPrefab.name;

        canvasInventario = Instantiate(canvasInventarioPrefab);
        canvasInventario.name = canvasInventarioPrefab.name;

        canvasFinPartida = Instantiate(canvasFinPartidaPrefab);
        canvasFinPartida.name = canvasFinPartidaPrefab.name;



    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       if (scene.name == "Nivel 1" || scene.name == "Nivel 2")
        {
            string[] temas = { "TemaInGame", "TemaInGame2" };
            FindObjectOfType<AudioManager>().PlayRandomTema(temas);
        }
    }

}
