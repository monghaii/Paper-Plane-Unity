﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private const float MOVE_AMOUNT = 25f;
    private Rigidbody2D planeBody;
    private SpriteRenderer planeSprite;
    private Transform planeTransform;
    private bool goingLeft = false;
    private bool planeMoved = false;
    
    // Start is called before the first frame update
    void Start(){
        planeTransform = GetComponent<Transform>();
    }

    private void Awake(){
        planeBody = GetComponent<Rigidbody2D>();
        planeSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(planeMoved == false){
                // special case; no velocity delay needed
                StartMovement();
                planeMoved = true;
                goingLeft = true;
            } else if(goingLeft){
                MoveRight();
                goingLeft = false;
            } else {
                MoveLeft();
                goingLeft = true;
            } 
        }
    }

    // movement functions
    private void MoveLeft(){

        // TODO: make this velocity responsive to current X velocity

        //planeBody.velocity = new Vector2(15f, 0);
        //yield return new WaitForSeconds(1);
        planeBody.velocity = new Vector2(-1 * MOVE_AMOUNT, 0);
        planeTransform.localScale = new Vector3(8, 8, 8);
    }

    private void MoveRight(){
        //planeBody.velocity = new Vector2(-15f, 0);
        //yield return new WaitForSeconds(1);
        planeBody.velocity = new Vector2(MOVE_AMOUNT, 0);
        planeTransform.localScale = new Vector3(-8, 8, 8);
    }

    // called when first tap is registered
    private void StartMovement(){
        Level.GetInstance().StartLevel();
        planeBody.velocity = new Vector2(-1 * MOVE_AMOUNT, 0);
    }

    // game over trigger
    private void OnTriggerEnter2D(Collider2D collision){
        planeBody.velocity = new Vector2(0f, 0f);
        planeBody.gravityScale = 5f;
        Level.GetInstance().StopLevel();
        Debug.Log("dead!");
    }

}



// to reload game scene
// UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");