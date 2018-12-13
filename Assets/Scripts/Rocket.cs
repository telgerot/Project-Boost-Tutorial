using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody myRigidBody;

    [Header("Config")]
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;


    [Header("SFX")]
    [SerializeField] AudioSource rocketSound;


	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly collision");
                break;

            default:
                Debug.Log("Hostile collision");
                break;
                               
        }
    }
    private void ProcessInput()
    {
        Thrust();
        Rotate();
        GhettoZAxisFix();  //to prevent the rocket from going to/from the player camera in this essentially-2D game
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            float thrustThisFrame = mainThrust * Time.deltaTime;
            myRigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!rocketSound.isPlaying) //so the sound effect doesn't layer
            {
                rocketSound.Play();
            }
        }
        else
        {
            rocketSound.Stop();
        }
    }

    private void Rotate()
    {
        myRigidBody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        myRigidBody.freezeRotation = false;
    }

    private void GhettoZAxisFix()
    {
        var XPos = transform.position.x;
        var YPos = transform.position.y;
        transform.position = new Vector3(XPos, YPos, 0);
    }
       
}
