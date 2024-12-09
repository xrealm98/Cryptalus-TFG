using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGrid : MonoBehaviour
{

    /*private void Awake() {

        EditarNivel();
    }*/

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
        
        //yield return new WaitForSeconds(0.2f);

       // EnemigoSpawn enemigoSpawn = GetComponentInChildren<EnemigoSpawn>();

       /* if (enemigoSpawn != null)
        {
            // Llamamos a SpawnEnemigos desde EnemigoSpawn
            enemigoSpawn.SpawnEnemigos();
        }
        else
        {
            Debug.LogError("No se encontró el componente EnemigoSpawn en los hijos de Grid.");
        }*/

       // PlayerYEscalerasSpawn playerYEscalerasSpawn = GetComponentInChildren<PlayerYEscalerasSpawn>();

       

    }
}
