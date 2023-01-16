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
       
    }
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.gameObject.tag);
            Conversation();
            Destroy(gameObject);

        }
        
    }

    void Conversation()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }
}
