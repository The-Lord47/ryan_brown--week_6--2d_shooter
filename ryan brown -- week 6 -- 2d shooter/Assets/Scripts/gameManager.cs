using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class gameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject[] splatterFXs;
    float spawnRate = 0.5f;

    public int score;
    public int lives = 3;
    public bool gameActive = true;
    public bool gamePaused;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
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
            yield return new WaitForSeconds(spawnRate);
            int targetIndex = Random.Range(0, targets.Count);
            Instantiate(targets[targetIndex], transform);
        }
    }
}
