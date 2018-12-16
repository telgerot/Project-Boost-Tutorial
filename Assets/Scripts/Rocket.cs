using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody myRigidBody;


    enum State {Alive, Dying, Transcending};
    [SerializeField] State state = State.Alive;
    [SerializeField] bool mortality = true;

    [Header("Parameters")]
    [SerializeField] float levelTransitionTime = 1f;

    [Header("Config")]
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    [Header("VFX")]
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    [Header("SFX")]
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip nextLevelSound;

    AudioSource rocketSound;
    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
        state = State.Alive;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessInput();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();                
                break;
            default:
                StartDeathSequence();
                break;
        }       
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        rocketSound.PlayOneShot(nextLevelSound);
        successParticles.Play();
        Invoke("LoadNextLevel", levelTransitionTime);
    }

    private void StartDeathSequence()
    {
        if (mortality == true)
        {
            state = State.Dying;
            rocketSound.Stop();
            mainEngineParticles.Stop();
            rocketSound.PlayOneShot(deathSound);
            deathParticles.Play();
            Invoke("LoadFirstLevel", levelTransitionTime);
        }
        else
        {
            return;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelSceneIndex = currentSceneIndex + 1;
        if (nextLevelSceneIndex < SceneManager.sceneCountInBuildSettings) // loops to first level after finishing the last
        {
            SceneManager.LoadScene(nextLevelSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ProcessInput()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotationInput();
        }
        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
        
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            rocketSound.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        myRigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        if (!rocketSound.isPlaying) //so the sound effect doesn't layer
        {
            rocketSound.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotationInput()
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

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            mortality = !mortality;
        }
    }
}
