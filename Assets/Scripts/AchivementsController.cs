using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;

public class AchivementsController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] imgColor;
    [SerializeField]
    public GameObject[] imgCHB;

    private int allNotes = 8;
    private int allBosses = 2;
    private int allClosedLocations = 2;
    private int allMagicTypes = 3;
    private int allSecretChests = 2;

    void Start()
    {
        Check20Deaths(); //1) more than 20 deaths since last save
        CheckChapterDying(); //2) complete the chapter without dying
        CheckNotes(); //3) collect all the notes
        CheckKilledBosses(); //4) kill all the bosses
        CheckLocations(); //5) visit all the closed locations
        CheckBossHeals(); //6) complete the boss without using a heal
        CheckMagic(); //7) Use all types of magic in one fight
        CheckChests(); //8) Collect all the secret chests
    }

    //1) More than 20 deaths since last save
    void Check20Deaths()
    {
        if (SaveSystem.GetInt("countOfDeaths") > 20)
        {
            imgCHB[0].SetActive(false);
            imgColor[0].SetActive(true);
        }
    }

    //2) Complete the chapter without dying
    void CheckChapterDying()
    {
        if (SaveSystem.GetInt("countOfDeaths") == 0)
        {
            imgCHB[1].SetActive(false);
            imgColor[1].SetActive(true);
        }
    }

    //3) Collect all the notes
    void CheckNotes()
    {
        if (SaveSystem.GetInt("countOfNotes") == allNotes)
        {
            imgCHB[2].SetActive(false);
            imgColor[2].SetActive(true);
        }
    }

    //4) Kill all the bosses
    void CheckKilledBosses()
    {
        if (SaveSystem.GetInt("countOfKilledBosses") == allBosses)
        {
            imgCHB[3].SetActive(false);
            imgColor[3].SetActive(true);
        }
    }

    //5) Visit all the closed locations
    void CheckLocations()
    {
        if (SaveSystem.GetInt("countOfVisitedLoc") == allClosedLocations)
        {
            imgCHB[4].SetActive(false);
            imgColor[4].SetActive(true);
        }
    }

    //6) Complete the boss without using a heal
    void CheckBossHeals()
    {
        if (SaveSystem.GetInt("countOfUsedHeals") == 0)
        {
            imgCHB[5].SetActive(false);
            imgColor[5].SetActive(true);
        }
    }

    //7) Use all types of magic in one fight
    void CheckMagic()
    {
        if (SaveSystem.GetInt("countOfMagic") == allMagicTypes)
        {
            imgCHB[6].SetActive(false);
            imgColor[6].SetActive(true);
        }
    }

    //8) Collect all the secret chests
    void CheckChests()
    {
        if (SaveSystem.GetInt("countOfChests") == allSecretChests)
        {
            imgCHB[7].SetActive(false);
            imgColor[7].SetActive(true);
        }
    }
}
