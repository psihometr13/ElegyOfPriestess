using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public bool check =false;
    void Update() 
    {
        if(check== true) 
        { 
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            check = true;
        }

    }

}
