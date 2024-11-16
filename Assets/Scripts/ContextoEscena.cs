using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextoEscena : MonoBehaviour
{
    public static ContextoEscena instancia; 

    public string escenaPrevia; 

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
    }

    public void EstablecerEscenaPrevia(string nombreEscena)
    {
        escenaPrevia = nombreEscena;
    }

    public string ObtenerEscenaPrevia()
    {
        return escenaPrevia;
    }
}
