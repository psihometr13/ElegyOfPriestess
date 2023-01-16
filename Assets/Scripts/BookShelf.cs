using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BookShelf : MonoBehaviour
{
    bool check = false;
    bool check2 = false;
    public Transform target;
    public Transform shelf;
    public float speed = 5f;
    public GameObject door;
    [SerializeField] public Animator anim;

    private void Start()
    {
        door.GetComponent<TeleportSystem>().enabled = false;
        door.GetComponent<BoxCollider2D>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(check);
        if (Input.GetKeyDown(KeyCode.F) && check)
        {
            anim.SetTrigger("move");
            //float step = speed * Time.deltaTime;
            //shelf.transform.position = Vector3.MoveTowards(shelf.transform.position, new Vector3(target.position.x, shelf.transform.position.y, 0), step);
            door.GetComponent<TeleportSystem>().enabled = true;
            door.GetComponent<BoxCollider2D>().enabled = true;
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          check = true;
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        check = false;

    }
}
