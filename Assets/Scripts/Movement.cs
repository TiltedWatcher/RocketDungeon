using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    //parameters
    [SerializeField] float mainThrustStrength = 100f; 


    //constants
    const string BOOSTER_INPUT_KEY = "space";
    const string ROTATE_LEFT_INPUT_KEY = "a";
    const string ROTATE_RIGHT_INPUT_KEY = "d";

    //cached References
    Rigidbody rocketBody;
    
    void Start(){
        rocketBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        ProcessInput();
    }

    private void ProcessInput() {
        ProcessThrust();
        ProcessRotation();
        
    }

    private void ProcessRotation() {
        if (Input.GetKey(ROTATE_LEFT_INPUT_KEY)) {
            Debug.Log("Rotating left");
        } else if (Input.GetKey(ROTATE_RIGHT_INPUT_KEY)) {
            Debug.Log("Rotating right");
        }
    }

    private void ProcessThrust() {
        if (Input.GetKey(BOOSTER_INPUT_KEY)) {
            rocketBody.AddRelativeForce(Vector3.up * mainThrustStrength * Time.deltaTime);
            
        }
    }
}
