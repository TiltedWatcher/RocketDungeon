using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour{

    //parameters
    [SerializeField] float timerBeforeRespawn = 1f;

    [Header("VFX")]
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] ParticleSystem victoryVFX;

    [Header("SFX")]
    [SerializeField] AudioClip explosionOnCrash;
    [SerializeField] AudioClip successSound;
    [SerializeField] float sfxVolume = 0.6f;


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

    //debug Modifiers
    bool invincible;

    public bool Invincible {
        get => invincible;
        set => invincible=value;
    }


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
                if (invincible) {
                    break;
                }
                HandleCrash();
                break;
        }


    }

    public void LevelCompleted() {
        GetComponent<PlayerMovement>().Transitioning = true;
        transitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound, sfxVolume);
        var vfx = Instantiate(victoryVFX, transform);
        sceneLoader.loadNextScene(timerBeforeRespawn);
    }

    public void HandleCrash() {
        //TODO SFX & VFX
        transitioning = true;
        audioSource.Stop();
        var vfx = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionOnCrash, transform.position, sfxVolume);
        GetComponent<PlayerMovement>().Transitioning = true;
        sceneLoader.reloadScene(timerBeforeRespawn);
        Destroy(gameObject);
        Destroy(vfx, explosionVFX.main.duration + Mathf.Epsilon);
    }
}
