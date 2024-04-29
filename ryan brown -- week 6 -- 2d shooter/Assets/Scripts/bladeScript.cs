using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeScript : MonoBehaviour
{
    Camera _mainCam;
    bool slicing;
    Collider bladeCollider;
    TrailRenderer bladeTrail;

    public Vector3 bladeDirection;
    public float minSliceVelocity = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
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

}
