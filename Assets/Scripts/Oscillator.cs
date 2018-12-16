using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0,1)]float movementFactor;

    Vector3 startingPos;

	void Start () {
        startingPos = transform.position;
	}
	
	void Update () {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
        
	}
}
