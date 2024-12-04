using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instancia;

    public BibliotecaEquipoSO bibliotecaEquipoSO;
    
    private InventarioManager inventarioManager;

    public TMP_Text textoTooltip;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            instancia = this;
        }

    }
    void Start()
    {
        bibliotecaEquipoSO = GameObject.Find("CanvasInventario").GetComponent<BibliotecaEquipoSO>();
        inventarioManager = GameObject.Find("CanvasInventario").GetComponent<InventarioManager>();
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventarioManager.MenuInventario.activeSelf && !inventarioManager.MenuEquipamiento.activeSelf)
        {
            OcultarTooltip();
            return;
        }
        float offsetY = 5f;
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + offsetY);
    }

    public void MostrarTooltip(string nombreObjeto)
    {
        // Buscar el objeto en la base de datos
        ObjetoEquipamientoSO objetoEquipamiento = null;
        for (int i = 0; i < bibliotecaEquipoSO.equipamientoSO.Length; i++)
        {
            if (bibliotecaEquipoSO.equipamientoSO[i].nombreObjeto == nombreObjeto)
            {
                objetoEquipamiento = bibliotecaEquipoSO.equipamientoSO[i];
                break;
            }
        }

        // Si se encuentra el objeto, mostrar el tooltip
        if (objetoEquipamiento != null)
        {
            string tooltipText = $"{objetoEquipamiento.nombreObjeto}\n";

            if (objetoEquipamiento.ataque != 0)
                tooltipText += $"Ataque: {objetoEquipamiento.ataque}\n";
            if (objetoEquipamiento.vida != 0)
                tooltipText += $"Vida: {objetoEquipamiento.vida}\n";
            if (objetoEquipamiento.armadura != 0)
                tooltipText += $"Armadura: {objetoEquipamiento.armadura}\n";

            EditarYMostrarTooltip(tooltipText);
        }
    }
    public void EditarYMostrarTooltip(string mensaje)
    {
        gameObject.SetActive(true);
        textoTooltip.text = mensaje;

    }

    public void OcultarTooltip()
    {
        gameObject.SetActive(false);
        textoTooltip.text = string.Empty;

    }
}
