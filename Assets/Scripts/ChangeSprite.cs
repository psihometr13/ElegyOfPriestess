using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public RuntimeAnimatorController anim3;
    GameObject weapon;
    GameObject doorCamera;
    GameObject doorTeleport;
    public bool check =false;
    public bool check2 =false;

    private void Start()
    {
        
        doorTeleport = GameObject.FindGameObjectsWithTag("Teleporter")[0];
       if(!SaveSystem.GetBool("withWeapon"))
        {
            doorTeleport.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            doorTeleport.gameObject.GetComponent<TeleportSystem>().enabled = false;
        }

    }
    IEnumerator Waitfor()
    {
        yield return new WaitForSeconds(3f);
        this.GetComponent<Animator>().runtimeAnimatorController = anim3 as RuntimeAnimatorController;

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
                SaveSystem.SetBool("withWeapon", true);
            }
        }
        if (check2==true)
        {
            StartCoroutine("Waitfor");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            check = true;
            weapon = collision.gameObject;
        }
        if (collision.CompareTag("Sister"))
        {
            check2 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        check = false;
    }
}
