using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSpawn : MonoBehaviour
{
    public Tilemap spawnPlayerTilemap;

    void Start() {

        GameObject.Find("Player").transform.position = ObtenerPosicionesValidas()[0];
    
    }

    List<Vector3> ObtenerPosicionesValidas()
    {
        List<Vector3> posiciones = new List<Vector3>();

        // Iterar sobre las celdas del Tilemap de suelo
        BoundsInt bounds = spawnPlayerTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int celda = new Vector3Int(x, y, 0);

                // Verificar que hay un tile en el suelo y no en la pared
                if (spawnPlayerTilemap.HasTile(celda))
                {
                    // Convertir posición de celda a coordenadas del mundo
                    Vector3 posicionMundo = spawnPlayerTilemap.CellToWorld(celda) + spawnPlayerTilemap.tileAnchor;
                    posiciones.Add(posicionMundo);
                }
            }
        }

        return posiciones;
    }
}
