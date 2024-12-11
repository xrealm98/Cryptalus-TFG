using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGrid : MonoBehaviour
{


    public void EditarNivel()
    {
      
        int eje = Random.Range(0, 2);

        if (eje == 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y * -1, 1);
        }
        
        EnemigoSpawn enemigoSpawn = GetComponentInChildren<EnemigoSpawn>();

        if (enemigoSpawn != null)
        {
            // Llamar al método SpawnEnemigos después del flip
            enemigoSpawn.SpawnEnemigos();
        }
        else
        {
            Debug.LogError("No se encontró el componente EnemigoSpawn en los hijos de FlipGrid.");
        }



    }
}
