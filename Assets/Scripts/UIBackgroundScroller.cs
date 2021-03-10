using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackgroundScroller : MonoBehaviour{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    [SerializeField] float tolerance = 0.03f;

    Material backgroundMaterial;
    Vector2 offSet;

    void Start(){
        backgroundMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update(){

        ScrollBackground();

    }

    private void ScrollBackground() {
        backgroundMaterial.mainTextureOffset += offSet * Time.deltaTime;

        float currentOffset = backgroundMaterial.mainTextureOffset.y;
        int currentOffsetRounded = Mathf.RoundToInt(currentOffset);
        
        if (currentOffset != 0 && currentOffset + tolerance >= currentOffsetRounded && currentOffset <= currentOffsetRounded) {
            backgroundMaterial.mainTextureOffset = new Vector2(0,0);
        }

    }
}
