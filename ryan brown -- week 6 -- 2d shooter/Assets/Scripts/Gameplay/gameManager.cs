using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class gameManager : MonoBehaviour
{
    //--------------------PUBLIC VARIABLES--------------------
    [Header("Passable Variables")]
    public List<GameObject> targets;
    public GameObject[] splatterFXs;
    public int score;
    public int lives = 3;
    public bool gameActive = true;
    public bool gamePaused;
    public bool gameOver;
    public int comboTracker;

    //--------------------PRIVATE VARIABLES--------------------
    difficultyManager _dm;

    //--------------------START--------------------
    void Start()
    {
        //retrieves the difficulty manager script
        _dm = FindObjectOfType<difficultyManager>();
        //starts spawning targets
        StartCoroutine(SpawnTarget());
    }

    //--------------------UPDATE--------------------
    void Update()
    {
        //controls the game pausing and unpausing
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            gamePaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            gamePaused = false;
        }

        //triggers game over if the lives hit zero or go below
        if(lives <= 0)
        {
            gameOver = true;
        }
    }

    //spawns targets randomly
    IEnumerator SpawnTarget()
    {
        while (true)
        {
            //retrieves the time to wait between target spawns from the difficulty matrix
            yield return new WaitForSeconds(float.Parse(_dm.difficulty[_dm.difficultyIndex % 4][1]));
            int targetIndex = Random.Range(0, targets.Count);
            Instantiate(targets[targetIndex], transform);
        }
    }
}
