using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que gestiona la lógica de la tienda del juego. Interactua con GuardadoManager y EstadisticasPlayer.
/// </summary>
public class scriptTienda : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoMonedas, textoVida, textoAtaque, textoArmadura;
    [SerializeField] private Button botonVolver, botonVida, botonAtaque, botonArmadura;

    public class EstadisticaMejora
    {
        public float incremento;
        public int costoInicial;
        public int incrementoCosto;
        public int comprasRealizadas;
        public int limiteCompras;
    }

    private Dictionary<string, EstadisticaMejora> mejoras = new Dictionary<string, EstadisticaMejora>();

    /// <summary>
    /// Inicializa las mejoras disponibles y se actualiza la interfaz de la tienda.
    /// </summary>
    void Start()
    {
        InicializarMejoras();
        ActualizarTextoMonedas();
        InicializarTextosYBotones();
    }

    /// <summary>
    /// Inicializa los valores de las mejoras disponibles en la tienda.
    /// </summary>
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

    /// <summary>
    /// Actualiza el texto que muestra las monedas del jugador.
    /// </summary>
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

    /// <summary>
    /// Permite al jugador comprar una mejora si tiene suficientes monedas. Se guarda las compras realizadas en el archivo de guardado.
    /// También, llama a la función de desactivar botón en caso de alcanzar el limite de compras.
    /// </summary>
    /// <param name="nombreEstadistica">Nombre de la estadística a mejorar ("Vida", "Ataque", "Armadura").</param>
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
    
    /// <summary>
    /// Calcula el costo actual de una estadística específica.
    /// </summary>
    /// <param name="nombreEstadistica">Nombre de la estadística.</param>
    /// <returns>El costo actual de la mejora.</returns>
    private int ObtenerCosteMejora(string nombreEstadistica)
    {
        var mejora = mejoras[nombreEstadistica];
        return mejora.costoInicial + (mejora.comprasRealizadas * mejora.incrementoCosto);
    }

    /// <summary>
    /// Actualiza los texto de las mejoras de la tienda.
    /// </summary>
    /// <param name="nombreEstadistica">Nombre de la estadística.</param>
    /// <param name="costo">Costo actual de la mejora.</param>
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
        }
        else
        {
            texto = $"Costo: {ObtenerCosteMejora(nombreEstadistica)}    Aumento {nombreEstadistica.ToLower()}: {mejora.incremento}    {mejora.comprasRealizadas}/{mejora.limiteCompras}";
            textoComponentes[nombreEstadistica].text = texto;
            textoComponentes[nombreEstadistica].color = Color.white;
        }
    }



    /// <summary>
    /// Inicializa los textos y botones en la tienda.
    /// </summary>
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

    /// <summary>
    /// Aplica la mejora comprada a la estadística correspondiente y lo guarda en el archivo de guardado.
    /// </summary>
    /// <param name="nombreEstadistica">Nombre de la estadística a mejorar.</param>
    private void AplicarMejora(string nombreEstadistica)
    {
        var mejora = mejoras[nombreEstadistica];
        float estadisticaActual = GuardadoManager.instancia.ObtenerEstadisticaBase(nombreEstadistica);

        GuardadoManager.instancia.ActualizarEstadisticasBase(nombreEstadistica, estadisticaActual + mejora.incremento);
        GuardadoManager.instancia.GuardarDatos();
    }
    
    /// <summary>
    /// Desactiva el botón de mejora en la interfaz si se alcanza el límite de compras.
    /// </summary>
    /// <param name="nombreEstadistica">Nombre de la estadística.</param>
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
   
    /// <summary>
    /// Regresa al menú principal del juego.
    /// </summary>
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu Principal");

    }
}
