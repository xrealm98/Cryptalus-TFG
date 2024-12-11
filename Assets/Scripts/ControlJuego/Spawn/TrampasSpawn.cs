using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TrampasSpawn : MonoBehaviour
{
    public GameObject[] trampasPrefabs;
    public Tilemap spawnTrampasTilemap;
    public int cantidadMinimaTrampas = 5;

    void Start()
    {
        SpawnTrampas();
    }

    public void SpawnTrampas()
    {

        List<Vector3> posicionesSpawn = ObtenerPosicionesValidas();

        if (posicionesSpawn.Count == 0)
        {
            Debug.LogWarning("No hay posiciones válidas para spawnear enemigos.");
            return;
        }
        int cantidadMaximaTrampas = posicionesSpawn.Count;

        int cantidadTrampas = Random.Range(cantidadMinimaTrampas, cantidadMaximaTrampas + 1);

        for (int i = 0; i < cantidadTrampas; i++)
        {
            // Seleccionamos una posición aleatoria
            int indice = Random.Range(0, posicionesSpawn.Count);
            Vector3 posicion = posicionesSpawn[indice];

            // Evitamos spawnear múltiples enemigos en la misma posición quitando las posiciones de la lista.
            posicionesSpawn.RemoveAt(indice);

            // Seleccionamos un prefab de enemigo aleatorio en el array y lo instanciamos.
            GameObject trampaSeleccionado = trampasPrefabs[Random.Range(0, trampasPrefabs.Length)];
            Instantiate(trampaSeleccionado, posicion, Quaternion.identity);

        }

    }

    List<Vector3> ObtenerPosicionesValidas()
    {
        List<Vector3> posiciones = new List<Vector3>();

        // Iterar sobre las celdas del Tilemap de suelo
        BoundsInt bounds = spawnTrampasTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int celda = new Vector3Int(x, y, 0);

                // Verificamos que hay un tile en el suelo y no en la pared
                if (spawnTrampasTilemap.HasTile(celda))
                {
                    // Convertimos la posición de celda a coordenadas del mundo
                    Vector3 posicionMundo = spawnTrampasTilemap.CellToWorld(celda) + spawnTrampasTilemap.tileAnchor;
                    posiciones.Add(posicionMundo);
                }
            }
        }

        return posiciones;
    }
}
