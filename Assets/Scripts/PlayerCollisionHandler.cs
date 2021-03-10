using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour{

    //parameters
    [SerializeField] float timerBeforeRespawn = 1f;

    //constants
    const string ROCKET_COLLISION_ALLOWED_TAG_HARMLESS = "Friendly";
    const string ROCKET_COLLISION_ALLOWED_TAG_FINISH = "Finish";
    const string ROCKET_COLLISION_ALLOWED_TAG_FUEL = "Fuel";
    const string ROCKET_COLLISION_ALLOWED_TAG_POWERUP = "Powerup";

    //cached references
    SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }


    private void OnCollisionEnter(Collision collision) {

        switch (collision.gameObject.tag) {
            case ROCKET_COLLISION_ALLOWED_TAG_HARMLESS:
                Debug.Log("Collided with something friendly");
                break;

            case ROCKET_COLLISION_ALLOWED_TAG_FINISH:
                LevelCompleted();
                break;

            case ROCKET_COLLISION_ALLOWED_TAG_FUEL:
                Debug.Log("Collision with Fuel Pickup");
                break;
            case ROCKET_COLLISION_ALLOWED_TAG_POWERUP:
                Debug.Log("Yay a powerup");
                break;

            default:
                HandleCrash();
                break;
        }


    }

    private void LevelCompleted() {
        Debug.Log("Finished the Level");
        sceneLoader.loadNextScene(timerBeforeRespawn);
    }

    private void HandleCrash() {
        Time.timeScale = 0f;
        GetComponent<Movement>().enabled = false;
        sceneLoader.reloadScene(timerBeforeRespawn);
        Debug.Log("BOOOOOOOOOOOOOMMMMMMM");
    }
}
