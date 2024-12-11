using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemigoSpawn : MonoBehaviour
{
    public GameObject[] enemigosPrefabs;
    public Tilemap spawnEnemigoTilemap;

    public int cantidadMinimaEnemigos = 10;
    public float esperaAntesDeSpawn = 2f;


    void Start()
    {
        SpawnEnemigos();
    }

    public void SpawnEnemigos()
    {

        List<Vector3> posicionesSpawn = ObtenerPosicionesValidas();

        if (posicionesSpawn.Count == 0)
        {
            Debug.LogWarning("No hay posiciones válidas para spawnear enemigos.");
            return;
        }
        int cantidadMaximaEnemigos = posicionesSpawn.Count;

        int cantidadEnemigos = Random.Range(cantidadMinimaEnemigos, cantidadMaximaEnemigos + 1);

        for (int i = 0; i < cantidadEnemigos; i++)
        {
            // Seleccionamos una posición aleatoria
            int indice = Random.Range(0, posicionesSpawn.Count);
            Vector3 posicion = posicionesSpawn[indice];

            // Evitamos spawnear múltiples enemigos en la misma posición
            posicionesSpawn.RemoveAt(indice);

            // Seleccionamos un prefab de enemigo aleatorio en el array y lo instanciamos.
            GameObject enemigoSeleccionado = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Length)];
            Instantiate(enemigoSeleccionado, posicion, Quaternion.identity);

        }

    }

    List<Vector3> ObtenerPosicionesValidas()
    {
        List<Vector3> posiciones = new List<Vector3>();

        // Iteramos sobre las celdas del Tilemap de suelo
        BoundsInt bounds = spawnEnemigoTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int celda = new Vector3Int(x, y, 0);

                // Verificamos que hay un tile en el suelo y no en la pared
                if (spawnEnemigoTilemap.HasTile(celda))
                {
                    // Convertimos posición de celda a coordenadas del mundo
                    Vector3 posicionMundo = spawnEnemigoTilemap.CellToWorld(celda) + spawnEnemigoTilemap.tileAnchor;


                    posiciones.Add(posicionMundo);
                }
            }
        }

        return posiciones;
    }
}
