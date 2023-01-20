using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CloseGame");
    }

    IEnumerator CloseGame()
    {
        yield return new WaitForSeconds(7f);
        Application.Quit();

    }
}

