using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Unity.VisualScripting;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }
    public bool newGame = false;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartPressed()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }

    public void NewGame()
    {
        string rootFolder = @"C:\Users\ой\AppData\LocalLow\Dream\ElegyOfPriestess\";
        // Files to be deleted    
        string authorsFile = "Profile.bin";

        try
        {
            // Check if file exists with its full path    
            if (File.Exists(Path.Combine(rootFolder, authorsFile)))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(rootFolder, authorsFile));
                Console.WriteLine("File deleted.");
            }
            else Console.WriteLine("File not found");
        }
        catch (IOException ioExp)
        {
            Console.WriteLine(ioExp.Message);
        }

        newGame = true;
        SceneManager.LoadScene(2);
    }
}
