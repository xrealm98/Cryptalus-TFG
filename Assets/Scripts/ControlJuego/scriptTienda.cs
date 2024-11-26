using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scriptTienda  : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoMonedas, textoVida, textoAtaque, textoArmadura ;
    [SerializeField] private Button botonVolver,botonVida, botonAtaque, botonArmadura;
          
    public class EstadisticaMejora
    {
        public float incremento;
        public int costoInicial;
        public int incrementoCosto;
        public int comprasRealizadas;
        public int limiteCompras;
    }

    private Dictionary<string, EstadisticaMejora> mejoras = new Dictionary<string, EstadisticaMejora>();


    void Start()
    {
        InicializarMejoras();
        ActualizarTextoMonedas();
        InicializarTextosYBotones();
    }

    private void InicializarMejoras()
    {
        mejoras.Add("Vida", new EstadisticaMejora
        {
            incremento = 30f,
            costoInicial = 50,
            incrementoCosto = 20,
            comprasRealizadas = GuardadoManager.instancia.datosActuales.comprasVida,
            limiteCompras = 3
        });
        mejoras.Add("Ataque", new EstadisticaMejora
        {
            incremento = 5f,
            costoInicial = 30,
            incrementoCosto = 15,
            comprasRealizadas = GuardadoManager.instancia.datosActuales.comprasAtaque,
            limiteCompras = 5
        });
        mejoras.Add("Armadura", new EstadisticaMejora
        {
            incremento = 2f,
            costoInicial = 40,
            incrementoCosto = 10,
            comprasRealizadas = GuardadoManager.instancia.datosActuales.comprasArmadura,
            limiteCompras = 2
        });
    }

    public void ActualizarTextoMonedas()
    {
        if (textoMonedas != null)
        {
            
            textoMonedas.text = "Monedas: " + MonedasManager.GetMonedasTotal().ToString();
        }
        else
        {
            Debug.LogWarning("Texto de monedas no asignado en el Inspector.");
        }
    }


    public void ComprarMejora(string nombreEstadistica)
    {
        int costo = ObtenerCosteMejora(nombreEstadistica);

       
        if (MonedasManager.GetMonedasTotal() >= costo)
        {
           
            MonedasManager.AddMonedas(-costo);
            ActualizarTextoMonedas();

            AplicarMejora(nombreEstadistica);

            mejoras[nombreEstadistica].comprasRealizadas++;

            switch (nombreEstadistica)
            {
                case "Vida":
                    GuardadoManager.instancia.datosActuales.comprasVida = mejoras["Vida"].comprasRealizadas;
                    break;
                case "Ataque":
                    GuardadoManager.instancia.datosActuales.comprasAtaque = mejoras["Ataque"].comprasRealizadas;
                    break;
                case "Armadura":
                    GuardadoManager.instancia.datosActuales.comprasArmadura = mejoras["Armadura"].comprasRealizadas;
                    break;
            }
            GuardadoManager.instancia.GuardarDatos();


            if (mejoras[nombreEstadistica].comprasRealizadas >= mejoras[nombreEstadistica].limiteCompras)
            {
                // Desactivar el botón de mejora si el límite es alcanzado
                DesactivarBotonMejora(nombreEstadistica);
            }
            
            ActualizarTextoMejoras(nombreEstadistica, costo);

            Debug.Log($"{nombreEstadistica} mejorada. Costo actual: {costo}");
        }
        else
        {
            Debug.LogWarning("No tienes suficientes monedas para comprar esta mejora.");
        }
    }
    private int ObtenerCosteMejora(string nombreEstadistica)
    {
        var mejora = mejoras[nombreEstadistica];
        return mejora.costoInicial + (mejora.comprasRealizadas * mejora.incrementoCosto);
    }

    private void ActualizarTextoMejoras(string nombreEstadistica, int costo)
    {
        var mejora = mejoras[nombreEstadistica];
        string texto;
        // Mapeamos los textos y los introducimos en un diccionario
        var textoComponentes = new Dictionary<string, TextMeshProUGUI>
        {
            { "Vida", textoVida },
            { "Ataque", textoAtaque },
            { "Armadura", textoArmadura }
        };

        // Actualizamos directamente el texto y su color
        if (mejora.comprasRealizadas >= mejora.limiteCompras)
        {
            texto = "Ya has comprado todas las mejoras de este tipo.";                      
            textoComponentes[nombreEstadistica].text = texto;
            textoComponentes[nombreEstadistica].color = Color.red;
        }else{
            texto = $"Costo: {ObtenerCosteMejora(nombreEstadistica)}    Aumento {nombreEstadistica.ToLower()}: {mejora.incremento}    {mejora.comprasRealizadas}/{mejora.limiteCompras}";              
            textoComponentes[nombreEstadistica].text = texto;
            textoComponentes[nombreEstadistica].color = Color.white;
        }
    }

    private void InicializarTextosYBotones()
    {
       
        ActualizarTextoMejoras("Vida", ObtenerCosteMejora("Vida"));
        ActualizarTextoMejoras("Ataque", ObtenerCosteMejora("Ataque"));
        ActualizarTextoMejoras("Armadura", ObtenerCosteMejora("Armadura"));

       
        if (mejoras["Vida"].comprasRealizadas >= mejoras["Vida"].limiteCompras)
            DesactivarBotonMejora("Vida");

        if (mejoras["Ataque"].comprasRealizadas >= mejoras["Ataque"].limiteCompras)
            DesactivarBotonMejora("Ataque");

        if (mejoras["Armadura"].comprasRealizadas >= mejoras["Armadura"].limiteCompras)
            DesactivarBotonMejora("Armadura");
    }

    private void AplicarMejora(string nombreEstadistica)
    {
        var mejora = mejoras[nombreEstadistica];
        float estadisticaActual = GuardadoManager.instancia.ObtenerEstadisticaBase(nombreEstadistica);

        GuardadoManager.instancia.ActualizarEstadisticasBase(nombreEstadistica, estadisticaActual + mejora.incremento);
        GuardadoManager.instancia.GuardarDatos();
    }

    private void DesactivarBotonMejora(string nombreEstadistica)
    {
        switch (nombreEstadistica)
        {
            case "Vida":
                botonVida.interactable = false;
                break;
            case "Ataque":
                botonAtaque.interactable = false;
                break;
            case "Armadura":
                botonArmadura.interactable = false;
                break;
        }
    }

    public void VolverAlMenu() {
       SceneManager.LoadScene("Menu Principal");

    }
}
