using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }
    public bool newGame = false;

    public void StartPressed()
    {
        SceneManager.LoadScene(2);
    }

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

    public void NewGamePressed()
    {
        string rootFolder = @"C:\Users\Beebo\AppData\LocalLow\Dream\ElegyOfPriestess\";
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
        SceneManager.LoadScene(1);
    }


    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
}
