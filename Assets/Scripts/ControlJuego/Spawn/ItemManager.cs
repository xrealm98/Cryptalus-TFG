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
        // Determinar si el objeto que caerá será un equipamiento o un consumible. Actualmente 50%/50%.
        bool esConsumible = Random.Range(0f, 1f) > 0.5f;

        if (esConsumible)
        {
            GenerarConsumible(posicionEnemigo);
        }
        else {
            GenerarObjetoEquipamiento(posicionEnemigo);
        
        }

    }
    void GenerarConsumible(Vector3 posicionEnemigo)
    {
        // Seleccionamos un consumible aleatorio
        ObjetoSO consumibleSO = bibliotecaSO.consumibleSO[Random.Range(0, bibliotecaSO.consumibleSO.Length)];

        // Creamos el gameobject para el consumible
        GameObject nuevoConsumible = Instantiate(objetoPrefab, posicionEnemigo, Quaternion.identity);

        nuevoConsumible.layer = LayerMask.NameToLayer("Objetos");

        // Asignamos las propiedades al objeto consumible
        Transform hijoSprite = nuevoConsumible.transform.GetChild(0);
        SpriteRenderer spriteRenderer = hijoSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = consumibleSO.sprite;

        Objeto objeto = nuevoConsumible.GetComponent<Objeto>();
        if (objeto != null)
        {
            objeto.nombreObjeto = consumibleSO.nombreObjeto;
            objeto.cantidad = Random.Range(1, 4);
            objeto.sprite = consumibleSO.sprite;
            objeto.descripcionObjeto = consumibleSO.descripcion;
            objeto.tipoObjeto = consumibleSO.tipoObjeto;
        }

        nuevoConsumible.transform.localScale = consumibleSO.escala;
    }


    void GenerarObjetoEquipamiento(Vector3 posicionEnemigo)
    {
        List<ObjetoEquipamientoSO> objetosFiltrados = FiltrarObjetosPorNivel(estadisticasPlayer.nivelPlayer);

        if (objetosFiltrados.Count > 0)
        {
            // Se selecciona uno de los objetos en el rango de niveles del jugador.
            ObjetoEquipamientoSO objetoSO = objetosFiltrados[Random.Range(0, objetosFiltrados.Count)];

            // Generamos el objeto en el nivel.
            // Creamos el gameobject a través de un prefab.
            GameObject nuevoObjeto = Instantiate(objetoPrefab, posicionEnemigo, Quaternion.identity);

            // Se asigna el nombre
            nuevoObjeto.name = objetoSO.nombreObjeto;
            nuevoObjeto.layer = LayerMask.NameToLayer("Objetos");

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

            nuevoObjeto.transform.localScale = objetoSO.escala;
        }
        else
        {
            Debug.LogWarning("No se encontraron objetos válidos para el nivel del jugador.");
        }



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