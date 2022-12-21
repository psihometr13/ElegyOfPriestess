using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    //Dialog
    public NPCConversation myConversation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Player")
        {
            Conversation();
            Destroy(gameObject);
        }
    }

    void Conversation()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }
}
