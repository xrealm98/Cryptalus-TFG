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

    public void SpawnPlayerYEscaleras()
    {
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
            int indiceJugador = Random.Range(0, posicionesValidas.Count);
            Vector3 posicionJugador = posicionesValidas[indiceJugador];

            if (posicionJugador != Vector3.zero)
            {
                // Posicionar al jugador
                GameObject jugador = GameObject.Find("Player");
                if (jugador != null)
                {
                    jugador.transform.position = posicionJugador;
                }

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

                    // Verificamos que hay un tile en el suelo.
                    if (spawnPlayerYEscaleraTilemap.HasTile(celda))
                    {
                        // Convertimos la posición de celda a coordenadas del mundo
                        Vector3 posicionMundo = spawnPlayerYEscaleraTilemap.CellToWorld(celda) + spawnPlayerYEscaleraTilemap.tileAnchor;
                        posiciones.Add(posicionMundo);
                    }
                }
            }

            return posiciones;
        }


    }
}
