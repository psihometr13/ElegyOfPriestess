using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCont : MonoBehaviour
{
    public bool check=false;
    public string name;
    public static CameraCont Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CamTP"))
        {
            name = collision.gameObject.name;
            check = true;
            
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CamTP"))
        {
            if (collision.gameObject == check)
            {
                check = false;
               
            }
        }

    }

}
