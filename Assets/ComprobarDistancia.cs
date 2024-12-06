using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobarDistancia : MonoBehaviour
{
    private GameObject player;
    public bool isColliding = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject == player.gameObject)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject == player.gameObject)
        {
            isColliding = false;
        }
    }



}
