using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour{

    //parameters
    [SerializeField] float timerBeforeRespawn = 1f;
    [SerializeField] AudioClip explosionOnCrash;
    [SerializeField] float sfxVolume = 0.6f;
    [SerializeField] AudioClip successSound;

    //constants
    const string ROCKET_COLLISION_ALLOWED_TAG_HARMLESS = "Friendly";
    const string ROCKET_COLLISION_ALLOWED_TAG_FINISH = "Finish";
    const string ROCKET_COLLISION_ALLOWED_TAG_FUEL = "Fuel";
    const string ROCKET_COLLISION_ALLOWED_TAG_POWERUP = "Powerup";

    //cached references
    SceneLoader sceneLoader;
    AudioSource audioSource;

    //states
    bool transitioning = false;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {

    }

    private void OnCollisionEnter(Collision collision) {

        if (transitioning) {
            return;
        }

        switch (collision.gameObject.tag) {
            case ROCKET_COLLISION_ALLOWED_TAG_HARMLESS:
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
        GetComponent<PlayerMovement>().Transitioning = true;
        transitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound, sfxVolume);
       
        //TODO SFX & VFX
        //Time.timeScale = 0f;
        sceneLoader.loadNextScene(timerBeforeRespawn);
    }

    private void HandleCrash() {
        //TODO SFX & VFX
        transitioning = true;
        audioSource.Stop();
        AudioSource.PlayClipAtPoint(explosionOnCrash, transform.position, sfxVolume);
        GetComponent<PlayerMovement>().Transitioning = true;
        //Time.timeScale = 0f;
        sceneLoader.reloadScene(timerBeforeRespawn);
        Destroy(gameObject);
    }
}
