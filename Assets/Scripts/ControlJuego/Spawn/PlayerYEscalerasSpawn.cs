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
            // Seleccionamos una posici�n aleatoria para la escalera
            int indiceEscalera = Random.Range(0, posicionesValidas.Count);
            Vector3 posicionEscalera = posicionesValidas[indiceEscalera];

            // Instanciar la escalera
            Instantiate(escaleraPrefab, posicionEscalera, Quaternion.identity);

            // Remover la posici�n de la escalera para evitar que el jugador spawnee en ella
            posicionesValidas.RemoveAt(indiceEscalera);

            // Seleccionar una posici�n aleatoria para el jugador que cumpla con la distancia m�nima
            Vector3 posicionJugador = SeleccionarPosicionJugador(posicionesValidas, posicionEscalera);

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
                        // Convertimos la posici�n de celda a coordenadas del mundo
                        Vector3 posicionMundo = spawnPlayerYEscaleraTilemap.CellToWorld(celda) + spawnPlayerYEscaleraTilemap.tileAnchor;
                        posiciones.Add(posicionMundo);
                    }
                }
            }

            return posiciones;
        }

        Vector3 SeleccionarPosicionJugador(List<Vector3> posiciones, Vector3 posicionEscalera)
        {
            // Creamos una lista de posiciones que cumplen con la distancia m�nima para spawnear el jugador a distancia de la escalera.
            List<Vector3> posicionesFiltradas = posiciones.FindAll(posicion =>
                Vector3.Distance(posicion, posicionEscalera) >= distanciaMinima);

            if (posicionesFiltradas.Count > 0)
            {
                // Elegimos una posici�n aleatoria entre las v�lidas
                int indiceAleatorio = Random.Range(0, posicionesFiltradas.Count);
                return posicionesFiltradas[indiceAleatorio];
            }

            // Si no hay posiciones que cumplan con la distancia m�nima, se devolver� Vector3.zero
            return Vector3.zero;
        }

    }
}
