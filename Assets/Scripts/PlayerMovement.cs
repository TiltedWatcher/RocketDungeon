using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    //parameters
    [SerializeField] float mainThrustStrength = 100f;
    [SerializeField] float rotationThrusterStrength = 10f;
    [SerializeField] AudioClip engineSound;


    //constants
    const string BOOSTER_INPUT_KEY = "space";
    const string ROTATE_LEFT_INPUT_KEY = "a";
    const string ROTATE_RIGHT_INPUT_KEY = "d";


    //cached References
    Rigidbody rocketBody;
    AudioSource rocketAudio;

    //states
    bool transitioning = false;

    public bool Transitioning {
        set => transitioning = value;
    }

    void Start(){
        rocketBody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        Debug.Log("This thing on?");
        if (!transitioning) {
            ProcessInput();
        } else{
            Debug.Log("Yo this is triggering");
            //rocketAudio.Stop();
        }
        
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
                rocketAudio.PlayOneShot(engineSound);
            }

        } else {
            if (rocketAudio.isPlaying) {
                rocketAudio.Pause();
            }
        }
    }
}
