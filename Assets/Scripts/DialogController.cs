using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    //Dialog
    public NPCConversation myConversation;

   
    private void Start()
    {
        if (SaveSystem.GetBool(gameObject.name)) {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SaveSystem.SetBool(gameObject.name, true);
            //Debug.Log(collision.gameObject.tag);
            Conversation();
            gameObject.GetComponent<BoxCollider2D>().enabled = false; 
        }
        
    }

    void Conversation()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }
}
