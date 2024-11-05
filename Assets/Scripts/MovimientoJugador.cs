using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidadJugador;
    Rigidbody2D rb;
    [HideInInspector]
    public float ultimoVectorHorizontal, ultimoVectorVertical;
    [HideInInspector]
    public Vector2 direccionMovimiento;
    Animator am;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        tomarInputs();
    }

    // Se usa fixedupdate, ya que este método no depende del ratio de frames.
    private void FixedUpdate()
    {
        movimiento();
    }

    void tomarInputs() {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        direccionMovimiento = new Vector2(movX,movY).normalized;
        
        if (direccionMovimiento.x != 0) 
        {
            ultimoVectorHorizontal = direccionMovimiento.x;
        }
        if (direccionMovimiento.y != 0)
        {
            ultimoVectorVertical = direccionMovimiento.y;
        }
       
        if (Input.GetMouseButtonDown(0))
        {
        
            am.SetTrigger("Ataque1");

        }
    }

    void movimiento() { 
        rb.velocity = new Vector2(direccionMovimiento.x * velocidadJugador, direccionMovimiento.y * velocidadJugador);

    }

}
