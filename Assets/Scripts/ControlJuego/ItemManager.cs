using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Referencia al prefab del equipamiento
    public GameObject objetoPrefab;

    // Referencia a la biblioteca de equipamiento SO y estadisticas del jugador.
    private BibliotecaEquipoSO bibliotecaSO;
    private EstadisticasPlayer estadisticasPlayer;

    void Start()
    {
        bibliotecaSO = GameObject.Find("CanvasInventario").GetComponent<BibliotecaEquipoSO>();
        estadisticasPlayer = GameObject.Find("Player").GetComponent<EstadisticasPlayer>();
    }

    public void GenerarDrop(Vector3 posicionEnemigo)
    {
        // Filtramos los objetos según el nivel del jugador.
        List<ObjetoEquipamientoSO> objetosFiltrados = FiltrarObjetosPorNivel(estadisticasPlayer.nivelPlayer);

        if (objetosFiltrados.Count > 0)
        {
            // Se selecciona uno de los objetos en el rango de niveles del jugador.
            ObjetoEquipamientoSO objetoSO = objetosFiltrados[Random.Range(0, objetosFiltrados.Count)];

            // Generamos el objeto en el nivel.
            CrearObjetoEquipamiento(objetoSO, posicionEnemigo);
        }
        else
        {
            Debug.LogWarning("No se encontraron objetos válidos para el nivel del jugador.");
        }
    }


    void CrearObjetoEquipamiento(ObjetoEquipamientoSO objetoSO, Vector3 posicionEnemigo)
    {

        // Creamos el gameobject a través de un prefab.
        GameObject nuevoObjeto = Instantiate(objetoPrefab, posicionEnemigo, Quaternion.identity);

        // Se asigna el nombre
        nuevoObjeto.name = objetoSO.nombreObjeto;

        // Accedemos al hijo con el SpriteRenderer
        Transform hijoSprite = nuevoObjeto.transform.GetChild(0);

        // Se verifica si el hijo tiene un SpriteRenderer
        SpriteRenderer spriteRenderer = hijoSprite.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = objetoSO.sprite;
      

        Objeto objeto = nuevoObjeto.GetComponent<Objeto>();
        if (objeto != null)
        {
            objeto.nombreObjeto = objetoSO.nombreObjeto;
            objeto.cantidad = 1;
            objeto.sprite = objetoSO.sprite;
            objeto.descripcionObjeto = objetoSO.descripcion;
            objeto.tipoObjeto = objetoSO.tipoObjeto;

        }

        nuevoObjeto.transform.localScale = objetoSO.tamaño;

    }

    List<ObjetoEquipamientoSO> FiltrarObjetosPorNivel(int nivelJugador)
    {
        List<ObjetoEquipamientoSO> objetosFiltrados = new();

        foreach (ObjetoEquipamientoSO objeto in bibliotecaSO.equipamientoSO)
        {
            // Filtrar los objetos que estén dentro del rango de nivel del jugador
            if (nivelJugador >= objeto.nivelMinimoDrop && nivelJugador <= objeto.nivelMaximoDrop)
            {
                objetosFiltrados.Add(objeto);

            }
        }

        return objetosFiltrados;
    }
}