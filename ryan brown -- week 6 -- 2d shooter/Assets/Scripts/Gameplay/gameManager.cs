using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class gameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject[] splatterFXs;

    public int score;
    public int lives = 3;
    public bool gameActive = true;
    public bool gamePaused;
    public bool gameOver;
    public int comboTracker;

    difficultyManager _dm;

    // Start is called before the first frame update
    void Start()
    {
        _dm = FindObjectOfType<difficultyManager>();
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            gamePaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            gamePaused = false;
        }

        if(lives <= 0)
        {
            gameOver = true;
        }
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(float.Parse(_dm.difficulty[_dm.difficultyIndex % 4][1]));
            int targetIndex = Random.Range(0, targets.Count);
            Instantiate(targets[targetIndex], transform);
        }
    }
}
