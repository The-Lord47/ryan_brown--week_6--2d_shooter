using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficultyManager : MonoBehaviour
{
    public int difficultyIndex = 4;
    public string[][] difficulty = new string[][]
    {
        new string[]{ "Easy", "1"},
        new string[]{ "Medium", "0.75" },
        new string[]{ "Hard", "0.5" },
        new string[]{ "Insane", "0.25" },
    };

    // Start is called before the first frame update
    void Start()
    {
        difficultyIndex = PlayerPrefs.GetInt("difficultyIndex");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("difficultyIndex") != difficultyIndex)
        {
            PlayerPrefs.SetInt("difficultyIndex", difficultyIndex);
        }
       
    }
}
