using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MonedasManager : MonoBehaviour
{

    private string pathGuardado;
    private int monedasTotal;
    public static MonedasManager instancia;

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

        // Asignamos la ruta del archivo de guardado
        pathGuardado = Application.persistentDataPath + "/guardado.dat";
    }
    private void Start() {
        monedasTotal = cargarMonedas();

    }


    public void AddMonedas(int monedas) {

        monedasTotal += monedas;

        Debug.Log("Monedas actuales: " + monedasTotal);


    }

    public int GetMonedasTotal() { return monedasTotal; }


    public void guardarMonedas()
    {
     
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(pathGuardado, FileMode.Create);
        formatter.Serialize(stream, monedasTotal);
        stream.Close();
        Debug.Log("Monedas guardadas correctamente en: " + pathGuardado);

    }

    public int cargarMonedas() {
        

        if (File.Exists(pathGuardado))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathGuardado, FileMode.Open);
            monedasTotal = (int)formatter.Deserialize(stream);
            Debug.Log("Monedas cargadas correctamente: " + monedasTotal);
            stream.Close();

        }
        else
        {
            monedasTotal = 0;
            Debug.LogWarning("No se encontró archivo. Comenzando con 0 monedas.");
            
        }
        return monedasTotal;

    }

    private void OnApplicationQuit()
    {
        guardarMonedas();
    }


}
