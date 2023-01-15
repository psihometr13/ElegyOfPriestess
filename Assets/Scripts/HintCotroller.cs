using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintCotroller : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] string hintText;
    [SerializeField] Text text;

    private void Start()
    {
        image.enabled = false;
        text.enabled = false;
    }

    //точность до секунды
    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(3.5f);

        image.enabled = false;
        text.enabled = false;
        Destroy(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            image.enabled = true;
            text.enabled = true;
            text.text = hintText;

            StartCoroutine("ExecuteAfterTime");
        }
    } 
}
