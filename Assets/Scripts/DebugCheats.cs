using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCheats : MonoBehaviour{

    bool invincible = false;
    bool noclipMode = false;

    PlayerCollisionHandler playerCol;


    private void Start() {
        playerCol = GetComponent<PlayerCollisionHandler>();
    }

    private void Update() {

        CheckInputDebugKeys();

    }

    private void CheckInputDebugKeys() {

        if (Input.GetKeyDown(KeyCode.L)) {
            playerCol.LevelCompleted();
        
        }

        if (Input.GetKeyDown(KeyCode.Hash)) {
            playerCol.HandleCrash();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            noclipMode = !noclipMode;
            var colliders = GetComponents<Collider>();

            foreach (Collider collider in colliders) {
                collider.enabled = !noclipMode;
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            invincible = !invincible;
            playerCol.Invincible = invincible;
        }

    }
}
