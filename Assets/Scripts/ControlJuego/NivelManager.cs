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
    public GameObject itemManagerPrefab;

    // Copias para instanciar los objetos y no trabajar sobre los Prefab
    private GameObject player;
    private GameObject canvasVida;
    private GameObject canvasInventario;
    private GameObject itemManager;

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
        }
    }

    // Cuando se llama buscara una escena aleatoria dentro del array y se cargará. Llama a IniciarNivel()
    public void IniciarPartida()
    {
        int nivelSeleccionado = Random.Range(0, niveles.Length);
        string nivel = niveles[nivelSeleccionado];
        SceneManager.LoadScene(nivel);

        StartCoroutine(IniciarNivel());

    }

    private IEnumerator IniciarNivel()
    {
        // Esperamos hasta que la escena se haya cargado completamente
        yield return new WaitForSeconds(0.1f); 

        // Buscar el objeto Grid en la escena
        GameObject gridObject = GameObject.Find("Grid");

        if (gridObject != null)
        {
            // Si encontramos el objeto Grid, aplicamos la inversión
            EditarNivel(gridObject);
        }
        else
        {
            Debug.LogError("El objeto 'Grid' no se encontró en la escena.");
        }
        
        // Si la partida no está iniciada, se cargarán todos los gameObject necesarios.
        if (!partidaIniciada) {
            CargarPersonaYInterfaz();
            partidaIniciada = true;
        }
    }

    // El método flipea los niveles para dar aleatoriedad y frescura a los niveles.
    public void EditarNivel(GameObject gridObject)
    {
        int eje = Random.Range(0, 2);

        if (eje == 0)
        {
            gridObject.transform.localScale = new Vector3(gridObject.transform.localScale.x * -1, 1, 1);
        }
        else
        {
            gridObject.transform.localScale = new Vector3(1, gridObject.transform.localScale.y * -1, 1);
        }
    }

    void CargarPersonaYInterfaz() {
        player = Instantiate(playerPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        player.name = playerPrefab.name;

        canvasVida = Instantiate(canvasVidaPrefab);
        canvasVida.name = canvasVidaPrefab.name;

        canvasInventario = Instantiate(canvasInventarioPrefab);
        canvasInventario.name = canvasInventarioPrefab.name;

        itemManager = Instantiate(itemManagerPrefab);
        itemManager.name = itemManagerPrefab.name;
    }

}
