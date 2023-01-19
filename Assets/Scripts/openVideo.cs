using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openVideo : MonoBehaviour
{
    public void ScenePontiff()
    {
        Upd_PlayerControl.Instance.SaveGame();
        SceneManager.LoadScene(3);
    }
    public void SceneEnd()
    {
        Upd_PlayerControl.Instance.SaveGame();
        SceneManager.LoadScene(5);
    }
}
