using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonedasManager {


    private static int monedasTotal;

    public static void Inicializar(int monedasIniciales)
    {
        monedasTotal = monedasIniciales;
        Debug.Log("Monedas iniciales: " + monedasTotal);
    }


    public static void AddMonedas(int monedas) {

        monedasTotal += monedas;

        Debug.Log("Monedas actuales: " + monedasTotal);


    }

    public static int GetMonedasTotal() { return monedasTotal; }

    

}
