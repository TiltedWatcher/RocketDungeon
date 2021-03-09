using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour{

    //parameters
    [SerializeField] float timerBeforeRespawn = 1f;

    //constants
    const string ROCKET_COLLISION_ALLOWED_TAG_ONE = "Friendly";
    const string ROCKET_COLLISION_ALLOWED_TAG_TWO = "Finish";
    const string ROCKET_COLLISION_ALLOWED_TAG_THREE = "Fuel";
    const string ROCKET_COLLISION_ALLOWED_TAG_FOUR = "Powerup";

    //cached references
    SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }


    private void OnCollisionEnter(Collision collision) {

        switch (collision.gameObject.tag) {
            case ROCKET_COLLISION_ALLOWED_TAG_ONE:
                Debug.Log("Collided with something friendly");
                break;

            case ROCKET_COLLISION_ALLOWED_TAG_TWO:
                Debug.Log("Finished the Level");
                break;

            case ROCKET_COLLISION_ALLOWED_TAG_THREE:
                Debug.Log("Collision with Fuel Pickup");
                break;
            case ROCKET_COLLISION_ALLOWED_TAG_FOUR:
                Debug.Log("Yay a powerup");
                break;

            default:
                HandleCrash();
                
                break;
        }


    }

    private void HandleCrash() {
        sceneLoader.reloadScene(timerBeforeRespawn);
        Debug.Log("BOOOOOOOOOOOOOMMMMMMM");
    }
}
