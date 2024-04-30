using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeScript : MonoBehaviour
{
    Camera _mainCam;
    bool slicing;
    bool canMakeSound = true;
    Collider bladeCollider;
    TrailRenderer bladeTrail;

    public Vector3 bladeDirection;
    public float minSliceVelocity = 0.01f;

    audioManager _am;

    // Start is called before the first frame update
    void Start()
    {
        _am = FindObjectOfType<audioManager>();

        bladeCollider = GetComponent<Collider>();
        _mainCam = Camera.main;
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }

        if (bladeDirection.magnitude / Time.deltaTime > 50 && canMakeSound)
        {
            StartCoroutine(sliceSound());
            canMakeSound = false;
        }
    }

    void StartSlicing()
    {
        Vector3 newPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;
        transform.position = newPos;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }
    void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }
    void ContinueSlicing()
    {
        Vector3 newPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;

        bladeDirection = newPos - transform.position;

        float velocity = bladeDirection.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPos;
    }

    IEnumerator sliceSound()
    {
        _am.bladeSFX.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        _am.bladeSFX.GetComponent<AudioSource>().PlayOneShot(_am.bladeSFXclips[Random.Range(0, _am.bladeSFXclips.Length)]);
        yield return new WaitForSeconds(0.5f);
        canMakeSound = true;
    }

}
