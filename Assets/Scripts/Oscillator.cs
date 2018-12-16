using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period;

    [SerializeField][Range(0,1)]float movementFactor;

    Vector3 startingPos;

	void Start () {
        startingPos = transform.position;
	}
	
	void Update () {
        float cycles = Time.time / period;

        if (period <= Mathf.Epsilon)
        {
            return;
        }
        else
        {
            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);

            movementFactor = (rawSinWave / 2f) + 0.5f;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;
        }
        
	}
}
