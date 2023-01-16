using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ItemsPickUp : MonoBehaviour
{
    bool check = false;

    void Update()
    {
        if (check == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (gameObject.tag == "heal")
                {
                    Upd_PlayerControl.Instance.maxHealth += 10;
                }
                if (gameObject.tag == "energy")
                {
                    Upd_PlayerControl.Instance.maxEnergy += 10;
                }
                Destroy(this);
                Destroy(this.gameObject.GetComponent<SpriteRenderer>());
                //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                check = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            check = true;

        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        check = false;
    }
}
