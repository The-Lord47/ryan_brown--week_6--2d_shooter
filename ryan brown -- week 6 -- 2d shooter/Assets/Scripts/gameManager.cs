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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
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
