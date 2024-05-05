using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeScript : MonoBehaviour
{
    //--------------------PRIVATE VARIABLES--------------------
    Camera _mainCam;
    bool slicing;
    bool canMakeSound = true;
    Collider bladeCollider;
    TrailRenderer bladeTrail;
    audioManager _am;

    //--------------------PUBLIC VARIABLES--------------------
    [Header("Referencables")]
    public Vector3 bladeDirection;

    [Header("Inspector Editables")]
    public float minSliceVelocity = 0.01f;



    //--------------------START
    void Start()
    {
        //sets up references
        _am = FindObjectOfType<audioManager>();
        bladeCollider = GetComponent<Collider>();
        _mainCam = Camera.main;
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        //ensures that the trail effect does not cut across the screen after previous mouse up
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    //--------------------UPDATE--------------------
    void Update()
    {
        //starts slicing on mouse down
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        //stops slicing on mouse up
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        //continues slicing in any other case
        else if (slicing)
        {
            ContinueSlicing();
        }

        //makes a blade sound if its velocity is high enough
        if (bladeDirection.magnitude / Time.deltaTime > 50 && canMakeSound)
        {
            StartCoroutine(sliceSound());
            canMakeSound = false;
        }
    }

    //--------------------START SLICING--------------------
    void StartSlicing()
    {
        //gets the mouse position to world position
        Vector3 newPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;
        //sets the blade to the mouse position
        transform.position = newPos;

        //sets the blade up to start slicing
        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    //--------------------STOP SLICING--------------------
    void StopSlicing()
    {
        //disables all the slicing mechanics of the blade
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    //--------------------CONTINUE SLICING--------------------
    void ContinueSlicing()
    {
        //gets the new world position of the mouse
        Vector3 newPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;

        //calculate where the blade will be going to get to the new world position
        bladeDirection = newPos - transform.position;
        //calculates a velocity based on the above
        float velocity = bladeDirection.magnitude / Time.deltaTime;

        //only enables the blade collider when the blade is travelling fast enough
        bladeCollider.enabled = velocity > minSliceVelocity;

        //updates blade position
        transform.position = newPos;
    }

    //--------------------SLICE SFX--------------------
    IEnumerator sliceSound()
    {
        //plays the blade sound effect at random pitches
        _am.bladeSFX.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        _am.bladeSFX.GetComponent<AudioSource>().PlayOneShot(_am.bladeSFXclips[Random.Range(0, _am.bladeSFXclips.Length)]);
        yield return new WaitForSeconds(0.5f);
        canMakeSound = true;
    }

}
