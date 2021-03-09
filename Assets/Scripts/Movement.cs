using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    //parameters
    [SerializeField] float mainThrustStrength = 100f;
    [SerializeField] float rotationThrusterStrength = 10f;


    //constants
    const string BOOSTER_INPUT_KEY = "space";
    const string ROTATE_LEFT_INPUT_KEY = "a";
    const string ROTATE_RIGHT_INPUT_KEY = "d";


    //cached References
    Rigidbody rocketBody;
    AudioSource rocketAudio;
    
    void Start(){
        rocketBody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
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
            ApplyRotation(rotationThrusterStrength);
        } else if (Input.GetKey(ROTATE_RIGHT_INPUT_KEY)) {
            ApplyRotation(-rotationThrusterStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame) {
        rocketBody.freezeRotation = true; // making sure rotation is not affected by physics
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketBody.freezeRotation = false; //rotation is affected by physics again
    }

    private void ProcessThrust() {
        if (Input.GetKey(BOOSTER_INPUT_KEY)) {
            rocketBody.AddRelativeForce(Vector3.up * mainThrustStrength * Time.deltaTime);

            if (!rocketAudio.isPlaying) {
                rocketAudio.Play();
            }

        } else {
            if (rocketAudio.isPlaying) {
                rocketAudio.Pause();
            }
        }
    }
}
