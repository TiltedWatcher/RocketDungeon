using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    const string BOOSTER_INPUT_KEY = "space";
    const string ROTATE_LEFT_INPUT_KEY = "a";
    const string ROTATE_RIGHT_INPUT_KEY = "d";

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        ProcessInput();
    }

    private void ProcessInput() {
        ProcessThrust();
        ProcessRotation();
    }

    private static void ProcessRotation() {
        if (Input.GetKey(ROTATE_LEFT_INPUT_KEY)) {
            Debug.Log("Rotating left");
        } else if (Input.GetKey(ROTATE_RIGHT_INPUT_KEY)) {
            Debug.Log("Rotating right");
        }
    }

    private static void ProcessThrust() {
        if (Input.GetKey(BOOSTER_INPUT_KEY)) {
            Debug.Log("Thrusters engaged!");
        }
    }
}
