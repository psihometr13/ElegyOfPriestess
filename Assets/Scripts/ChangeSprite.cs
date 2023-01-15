using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    GameObject weapon;
    GameObject doorCamera;
    GameObject doorTeleport;
    public bool check =false;

    private void Start()
    {
        
        doorTeleport = GameObject.FindGameObjectsWithTag("Teleporter")[0];

        doorTeleport.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        doorTeleport.gameObject.GetComponent<TeleportSystem>().enabled = false;
    }

    void Update() 
    {
        if(check== true) 
        { 
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
                Destroy(weapon);
                 doorTeleport.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            check = true;
            weapon = collision.gameObject;
        }

    }

}
