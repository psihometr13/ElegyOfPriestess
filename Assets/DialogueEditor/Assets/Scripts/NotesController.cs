using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesController : MonoBehaviour
{
    //public static NotesController Instance { get; private set; }

    GameObject note;
    [SerializeField] Image image;
    [SerializeField] string noteText;
    [SerializeField] Text text;

    bool check = false;

    private void Start()
    {
        SaveSystem.SetBool(gameObject.name, false);
        image.enabled = false;
        text.enabled = false;
    }

    void Update()
    {
        //Debug.Log(check);
        if (check == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Time.timeScale = 0;
                image.enabled = true;
                text.enabled = true;
                text.text = noteText;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Upd_PlayerControl.Instance.countOfNotes += 1;
                SaveSystem.SetBool(gameObject.name, true);
                Time.timeScale = 1;
                image.enabled = false;
                text.enabled = false;
                Destroy(this);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            check = true;
            note = collision.gameObject;
        }
    }
}
