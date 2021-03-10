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

    //states
    bool hasCrashed = false;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Update() {

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
        //TODO SFX & VFX
        Time.timeScale = 0f;
        sceneLoader.loadNextScene(timerBeforeRespawn);
    }

    private void HandleCrash() {
        //TODO SFX & VFX
        GetComponent<PlayerMovement>().HasCrashed = true;
        Time.timeScale = 0f;
        sceneLoader.reloadScene(timerBeforeRespawn);
    }
}
