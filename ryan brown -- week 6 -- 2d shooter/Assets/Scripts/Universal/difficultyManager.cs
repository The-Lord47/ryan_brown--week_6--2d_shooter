using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficultyManager : MonoBehaviour
{
    //--------------------PUBLIC VARIABLES--------------------
    [Header("Passables")]
    public int difficultyIndex = 4;
    public string[][] difficulty = new string[][]
    {
        new string[]{ "Easy", "1"},
        new string[]{ "Medium", "0.75" },
        new string[]{ "Hard", "0.5" },
        new string[]{ "Insane", "0.25" },
    };

    //--------------------START--------------------
    void Start()
    {
        //loads the difficulty from the playerpreferences
        difficultyIndex = PlayerPrefs.GetInt("difficultyIndex");
    }

    //--------------------UPDATE--------------------
    void Update()
    {
        //updates the diffiuclty if it is ever changed
        if(PlayerPrefs.GetInt("difficultyIndex") != difficultyIndex)
        {
            PlayerPrefs.SetInt("difficultyIndex", difficultyIndex);
        }
       
    }
}
