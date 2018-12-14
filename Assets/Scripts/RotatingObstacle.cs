using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour {

    [SerializeField] float rotationSpeed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
	}

    void Rotate()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationThisFrame);
    }
}
