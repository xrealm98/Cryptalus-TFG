using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;




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


    public void ActualizarEstadisticas(float ataqueBase, float vidaBase, float armaduraBase)
    {
        datosActuales.ataqueBase = ataqueBase;
        datosActuales.vidaBase = vidaBase;
        datosActuales.armaduraBase = armaduraBase;
    }

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

    public void GuardarDatos()
    {
        
        datosActuales.monedas = MonedasManager.GetMonedasTotal();
 
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(pathGuardado, FileMode.Create);
        formatter.Serialize(stream, datosActuales);
        stream.Close();
        Debug.Log("Datos guardados correctamente en: " + pathGuardado);
    }

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
