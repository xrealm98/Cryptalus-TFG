using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



/// <summary>
/// Clase que gestiona el almacenamiento de los datos del jugador.
/// </summary>

[System.Serializable]
public class DatosGuardados
{
    public int monedas;
    public float vidaBase;
    public float armaduraBase;
    public float ataqueBase;

    public int comprasVida;
    public int comprasAtaque;
    public int comprasArmadura;
}

public class GuardadoManager : MonoBehaviour
{
    public static GuardadoManager instancia;
    private string pathGuardado;
    
    public DatosGuardados datosActuales;


    /// <summary>
    /// Inicializa la instancia singleton y carga los datos almacenados.
    /// </summary>
    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        pathGuardado = Application.persistentDataPath + "/guardadoPartida.dat";
        CargarDatos();
        MonedasManager.Inicializar(datosActuales.monedas);
    }

    /// <summary>
    /// Actualiza las estadísticas base del jugador si no hay un guardado previo. Se llama desde EstadisticasPlayer
    /// </summary>
    /// <param name="ataqueBase">Nuevo valor del ataque base.</param>
    /// <param name="vidaBase">Nuevo valor de la vida base.</param>
    /// <param name="armaduraBase">Nuevo valor de la armadura base.</param>
    public void ActualizarEstadisticas(float ataqueBase, float vidaBase, float armaduraBase)
    {
        datosActuales.ataqueBase = ataqueBase;
        datosActuales.vidaBase = vidaBase;
        datosActuales.armaduraBase = armaduraBase;
    }

    /// <summary>
    /// Actualiza una estadística base específica del jugador cuando se compra en la tienda y se guarda.
    /// </summary>
    /// <param name="nombreEstadistica">Nombre de la estadística ("Vida", "Ataque", "Armadura").</param>
    /// <param name="nuevoValor">Nuevo valor de la estadística.</param>
    public void ActualizarEstadisticasBase(string nombreEstadistica, float nuevoValor)
    {
        switch (nombreEstadistica)
        {
            case "Vida":
                datosActuales.vidaBase = nuevoValor;
                break;
            case "Ataque":
                datosActuales.ataqueBase = nuevoValor;
                break;
            case "Armadura":
                datosActuales.armaduraBase = nuevoValor;
                break;
        }
        GuardarDatos();
    }

    /// <summary>
    /// Envía el valor de la estadistica actual a la tienda (scriptTienda).
    /// </summary>
    /// <param name="nombreEstadistica">Nombre de la estadística ("Vida", "Ataque", "Armadura").</param>
    /// <returns>Valor actual de la estadística.</returns>
    public float ObtenerEstadisticaBase(string nombreEstadistica)
    {
        switch (nombreEstadistica)
        {
            case "Vida":
                return datosActuales.vidaBase;
            case "Ataque":
                return datosActuales.ataqueBase;
            case "Armadura":
                return datosActuales.armaduraBase;
            default:
                Debug.LogWarning($"Estadística {nombreEstadistica} no encontrada.");
                return 0;
        }
    }
    
    /// <summary>
    /// Guarda los datos del jugador en la ruta establecida usando BinaryFormatter y filestream.
    /// Guarda las monedas, estadisticas mejoradas y el número de compras realizadas.
    /// </summary>
    /// <returns> Datos cargados o null.</returns>
    public void GuardarDatos()
    {
        
        datosActuales.monedas = MonedasManager.GetMonedasTotal();
 
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(pathGuardado, FileMode.Create);
        formatter.Serialize(stream, datosActuales);
        stream.Close();
        Debug.Log("Datos guardados correctamente en: " + pathGuardado);
    }
    /// <summary>
    /// Carga los datos del jugador desde el archivo de guardado usando BinaryFormatter y filestream.
    /// </summary>
    /// <returns> Datos cargados o null.</returns>
    public DatosGuardados CargarDatos()
    {
        if (File.Exists(pathGuardado))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathGuardado, FileMode.Open);
            datosActuales = (DatosGuardados)formatter.Deserialize(stream);
            stream.Close();
            Debug.Log("Datos cargados correctamente: Monedas = " + datosActuales.monedas);
            return datosActuales;
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado. Usando valores predeterminados.");
            return null;
           
            
        }
    }
   
    private void OnApplicationQuit()
    {
        GuardarDatos();
    }
}
