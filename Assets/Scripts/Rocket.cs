using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody myRigidBody;

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

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            myRigidBody.AddRelativeForce(Vector3.up);
            if (!rocketSound.isPlaying) //so the sound effect doesn't layer
            {
                rocketSound.Play();
            }
        }
        else
        {
            rocketSound.Stop();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward);
        }

        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
