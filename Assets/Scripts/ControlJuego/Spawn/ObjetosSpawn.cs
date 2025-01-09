using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjetosSpawn : MonoBehaviour
{
    public GameObject[] objetosPrefabs;
    public Tilemap spawnObjetosTilemap;
    public int cantidadMinimaObjetos = 3;

    void Start()
    {
        SpawnObjetos();
    }

    public void SpawnObjetos()
    {

        List<Vector3> posicionesSpawn = ObtenerPosicionesValidas();

        if (posicionesSpawn.Count == 0)
        {
            Debug.LogWarning("No hay posiciones válidas para spawnear objetos.");
            return;
        }
        int cantidadMaximaObjetos = posicionesSpawn.Count;

        int cantidadObjetos = Random.Range(cantidadMinimaObjetos, cantidadMaximaObjetos + 1);

        for (int i = 0; i < cantidadObjetos; i++)
        {
            // Seleccionamos una posición aleatoria
            int indice = Random.Range(0, posicionesSpawn.Count);
            Vector3 posicion = posicionesSpawn[indice];

            // Evitamos spawnear múltiples enemigos en la misma posición
            posicionesSpawn.RemoveAt(indice);

            // Seleccionamos un prefab de enemigo aleatorio en el array y lo instanciamos.
            GameObject objetoSeleccionado = objetosPrefabs[Random.Range(0, objetosPrefabs.Length)];
            Instantiate(objetoSeleccionado, posicion, Quaternion.identity);

        }

    }

    List<Vector3> ObtenerPosicionesValidas()
    {
        List<Vector3> posiciones = new List<Vector3>();

        // Iteramos sobre las celdas del Tilemap de suelo
        BoundsInt bounds = spawnObjetosTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int celda = new Vector3Int(x, y, 0);

                // Verificamos que hay un tile en el suelo.
                if (spawnObjetosTilemap.HasTile(celda))
                {
                    // Convertimos la posición de celda a coordenadas del mundo
                    Vector3 posicionMundo = spawnObjetosTilemap.CellToWorld(celda) + spawnObjetosTilemap.tileAnchor;
                    posiciones.Add(posicionMundo);
                }
            }
        }

        return posiciones;
    }
}
