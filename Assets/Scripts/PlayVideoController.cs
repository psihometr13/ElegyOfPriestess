using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayVideoController : MonoBehaviour
{
    [SerializeField] int sceneNum = 4;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
