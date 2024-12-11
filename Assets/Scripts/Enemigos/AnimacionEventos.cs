using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionEventos : MonoBehaviour
{
    private ComportamientoEnemigo comportamientoEnemigo;

    void Start() {
        comportamientoEnemigo = GetComponentInParent<ComportamientoEnemigo>();
    
    }
    public void Atacar() {
        comportamientoEnemigo.AtaqueAlJugador();
       
    }
    public void CambiarAIdle() {
        comportamientoEnemigo.CambioEstado(EstadoEnemigo.Idle);
    
    }

}
