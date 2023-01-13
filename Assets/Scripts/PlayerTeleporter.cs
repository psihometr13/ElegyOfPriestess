using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;
    public bool check;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter !=null)
            {
                transform.position = currentTeleporter.GetComponent<TeleportSystem>().GetDestination().position;    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter")) 
        {
            currentTeleporter = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter")) 
        {
            if (collision.gameObject == currentTeleporter)
            { 
                currentTeleporter = null; 
            }
        }
    }


}
