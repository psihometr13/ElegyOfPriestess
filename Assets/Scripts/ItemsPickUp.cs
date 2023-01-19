using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPickUp : MonoBehaviour
{
    bool check = false;

    private void Start()
    {
        if (SaveSystem.GetBool($"{gameObject.name}"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            SaveSystem.SetBool($"{gameObject.name}", false);
        }

    }
    void Update()
    {
        if (check == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (gameObject.tag == "heal" )
                {
                    SaveSystem.SetBool($"{gameObject.name}", true);
                    Upd_PlayerControl.Instance.maxHealth += 10;
                    Upd_PlayerControl.Instance.countOfUsedHeals += 1;
                }
                if (gameObject.tag == "energy")
                {
                    SaveSystem.SetBool($"{gameObject.name}", true);
                    Upd_PlayerControl.Instance.maxEnergy += 10;
                }
                //Destroy(this);
                //Destroy(this.gameObject.GetComponent<SpriteRenderer>());
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
