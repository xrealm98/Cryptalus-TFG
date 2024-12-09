using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerYEscalerasSpawn : MonoBehaviour
{
    public Tilemap spawnPlayerYEscaleraTilemap;
    public GameObject escaleraPrefab;
    public float distanciaMinima = 5f;

    void Start()
    {

        SpawnPlayerYEscaleras();
    }

    public void SpawnPlayerYEscaleras() {
        List<Vector3> posicionesValidas = ObtenerPosicionesValidas();

        if (posicionesValidas.Count > 1)
        {
            // Seleccionamos una posición aleatoria para la escalera
            int indiceEscalera = Random.Range(0, posicionesValidas.Count);
            Vector3 posicionEscalera = posicionesValidas[indiceEscalera];

            // Instanciar la escalera
            Instantiate(escaleraPrefab, posicionEscalera, Quaternion.identity);

            // Remover la posición de la escalera para evitar que el jugador spawnee en ella
            posicionesValidas.RemoveAt(indiceEscalera);

            // Seleccionar una posición aleatoria para el jugador que cumpla con la distancia mínima
            Vector3 posicionJugador = SeleccionarPosicionJugador(posicionesValidas, posicionEscalera);

            if (posicionJugador != Vector3.zero)
            {
                // Posicionar al jugador
                GameObject jugador = GameObject.Find("Player");
                if (jugador != null)
                {
                    jugador.transform.position = posicionJugador;
                }
                else
                {
                    Debug.LogError("No se encontró el jugador en la escena.");
                }
            }
            else
            {
                Debug.LogError("No se encontró una posición válida para el jugador que cumpla con la distancia mínima.");
            }
        }
        else
        {
            Debug.LogError("No hay suficientes posiciones válidas en el Tilemap para el jugador y la escalera.");
        }

    }

    List<Vector3> ObtenerPosicionesValidas()
    {
        List<Vector3> posiciones = new List<Vector3>();

        // Iterar sobre las celdas del Tilemap de suelo
        BoundsInt bounds = spawnPlayerYEscaleraTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int celda = new Vector3Int(x, y, 0);

                // Verificar que hay un tile en el suelo.
                if (spawnPlayerYEscaleraTilemap.HasTile(celda))
                {
                    // Convertimos posición de celda a coordenadas del mundo
                    Vector3 posicionMundo = spawnPlayerYEscaleraTilemap.CellToWorld(celda) + spawnPlayerYEscaleraTilemap.tileAnchor;
                    posiciones.Add(posicionMundo);
                }
            }
        }

        return posiciones;
    }
   
    Vector3 SeleccionarPosicionJugador(List<Vector3> posiciones, Vector3 posicionEscalera)
    {
        // Creamos una lista de posiciones que cumplen con la distancia mínima para spawnear el jugador a distancia de la escalera.
        List<Vector3> posicionesFiltradas = posiciones.FindAll(posicion =>
            Vector3.Distance(posicion, posicionEscalera) >= distanciaMinima);

        if (posicionesFiltradas.Count > 0)
        {
            // Elegir una posición aleatoria entre las válidas
            int indiceAleatorio = Random.Range(0, posicionesFiltradas.Count);
            return posicionesFiltradas[indiceAleatorio];
        }

        // Si no hay posiciones que cumplan con la distancia mínima, se devolverá Vector3.zero
        return Vector3.zero;
    }

}
