using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MenuNotesController : MonoBehaviour
{
    [SerializeField] List<GameObject> menuNotes = new List<GameObject>();
    public GameObject tooltipText;

    // Start is called before the first frame update
    void Update ()
    {
        foreach (GameObject note in menuNotes)
        {
            if (SaveSystem.GetBool(note.name) == true)
            {
                note.GetComponent<UnityEngine.UI.Text>().text = note.name;
                note.GetComponent<TooltipMenu>().enabled = true;
            }
            else
            {
                note.GetComponent<TooltipMenu>().enabled = false;
            }
        }
    }
}
