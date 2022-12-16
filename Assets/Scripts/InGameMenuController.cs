using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public void StartPressed()
    {
        PauseController.Instance.isPaused = !PauseController.Instance.isPaused;
        Time.timeScale = PauseController.Instance.isPaused ? 0 : 1;
        PauseController.Instance.pausePanel.SetActive(PauseController.Instance.isPaused);
        Debug.Log("Game continued...");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
}
