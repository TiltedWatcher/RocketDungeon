using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour{

    [SerializeField] Vector3 movementVector;
    [Tooltip("How long a single oscilation of this object will take in seconds")]
    [SerializeField] float oscilationPeriod = 5f;


    const float tau = Mathf.PI *2; //constant value of 6.283

    //cached references
    Vector3 startingPos;

    //states
    float movementFactor;

    void Start(){
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update() {

        if (oscilationPeriod <= Mathf.Epsilon) {
            return;
        }
        MoveObstacle();
    }

    private void MoveObstacle() {
        float cycles = Time.time / oscilationPeriod; //continually growing over time

        float rawSineWave = Mathf.Sin(tau * cycles); //cycling from -1 to 1
        movementFactor = (rawSineWave + 1f) /2f; // recalculated to move from 0 to 1, to make it cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
