using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class objectMovement : MonoBehaviour
{
    //--------------------PRIVATE VARIABLES--------------------
    float minSpeed = 5;
    float maxSpeed = 10;
    float maxTorque = 0.5f;
    float xRange = 8;
    float ySpawnPos = -2;
    Rigidbody rb;
    gameManager _gm;
    bladeScript _bs;
    audioManager _am;

    //--------------------START--------------------
    void Start()
    {
        //component and script references
        rb = GetComponent<Rigidbody>();
        _gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>();
        _bs = GameObject.FindGameObjectWithTag("Player").GetComponent<bladeScript>();
        _am = FindObjectOfType<audioManager>();

        //adds force and torque to the targets on spawn
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        //spawns the target in a random position at the base of the screen
        transform.position = RandomSpawnPos();
    }

    //--------------------UPDATE--------------------
    void Update()
    {
        //destroys the object if it goes below the screen
        if(transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }

    //--------------------RANDOM FORCE--------------------
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed,maxSpeed);
    }
    //--------------------RANDOM TORQUE--------------------
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    //--------------------RANDOM SPAWN POS--------------------
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    //--------------------COLLISION MECHANICS--------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //checks to see if the target collided with was a food item
            if (gameObject.CompareTag("redApple") || gameObject.CompareTag("greenApple") || gameObject.CompareTag("carrot") || gameObject.CompareTag("pear"))
            {
                //instantiates splatter particle fx for each food item
                if(gameObject.CompareTag("redApple"))
                {
                    Instantiate(_gm.splatterFXs[0], transform.position, Quaternion.LookRotation(_bs.bladeDirection), GameObject.Find("SplatterFX").transform);
                }
                if(gameObject.CompareTag("greenApple") || gameObject.CompareTag("pear"))
                {
                    Instantiate(_gm.splatterFXs[1], transform.position, Quaternion.LookRotation(_bs.bladeDirection), GameObject.Find("SplatterFX").transform);
                }
                if (gameObject.CompareTag("carrot"))
                {
                    Instantiate(_gm.splatterFXs[2], transform.position, Quaternion.LookRotation(_bs.bladeDirection), GameObject.Find("SplatterFX").transform);
                }
                //increases the score proportional to the combo tracker
                _gm.score += Mathf.FloorToInt((_gm.comboTracker+10)/10);
                //increases the combo tracker by one
                ++_gm.comboTracker;
                //adjusts the pitch of the following one shot for the food being sliced
                _am.fruitSFX.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                //plays a one shot of the food being sliced
                _am.fruitSFX.GetComponent<AudioSource>().PlayOneShot(_am.fruitSFXclips[Random.Range(0, _am.fruitSFXclips.Length)]);
            }
            //checks to see if the target collided with is bad
            else if(gameObject.CompareTag("bad"))
            {
                //removes a life
                --_gm.lives;
                //sets the combo tracker to 0
                _gm.comboTracker = 0;
                //instantiates an explosion particle effect
                Instantiate(_gm.splatterFXs[3], transform.position, Quaternion.Euler(-180,0,0), GameObject.Find("SplatterFX").transform);
                //plays a bomb explosion sound effect
                _am.bombSFX.GetComponent<AudioSource>().PlayOneShot(_am.bombSFXclip);
            }
            //destroys the game object regardless if it is good or bad
            Destroy(gameObject);
        }
    }

}
