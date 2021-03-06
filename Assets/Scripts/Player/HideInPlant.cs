﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInPlant : MonoBehaviour
{

    // Plant // 
    
    
    
    public bool foundPlant = false;
    public bool inPlant = false;
    private GameObject selectedPlant;
    private Vector3 foundPlantPos;
    public bool goingInPlant = false;
    private bool goingOutPlant = false;
    private float playerPlantdistance;

    // Player // 
    private GameObject Player;
    private PlayerControllerMovement canIMove;
    private Vector3 OldPlayerPos;
    public bool playerInvis = false;
    private Animator playerAnim;

    // Plant // 
    private Animator plantAnim;

    // Touch // 
    private GameObject plantHitCollider;
    private Vector3 mousePos;

    // Timer //
    private float timer = 2;
    private float jumpInOuttime = 6;
    private bool waittoClick = false;
    private float testtimer = 0;
    private bool canshake = true;
    private float plantTimer = 0;

    public Sprite startImg;
    public Sprite glowImg;





    void Start()
    {
        canIMove = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        Player = GameObject.Find("Player");
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }


    void Update()
    {
        // Has found a plant to go in // 
        if (foundPlant == true)
        {

            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {

                    if (touch.phase == TouchPhase.Began)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                        mousePos = new Vector3(mousePos.x, mousePos.y, plantHitCollider.transform.position.z);


                        if (plantHitCollider.GetComponent<PolygonCollider2D>().bounds.Contains(mousePos))
                        {
                            // If player isnt in plant and wants to go in //
                            if (inPlant == false && goingInPlant == false && waittoClick == false)
                            {
                                selectedPlant.transform.parent.GetComponent<SpriteRenderer>().color = new Color32(111, 111, 111, 255);
                                selectedPlant.transform.parent.GetComponent<Animator>().enabled = true;
                                canIMove.canMove = false;
                                OldPlayerPos = Player.transform.position;
                                playerPlantdistance = Vector3.Distance(foundPlantPos, OldPlayerPos) / 50;
                                goingInPlant = true;
                                playerAnim.SetInteger("PlayerAnimation", 9);
                                plantAnim.SetInteger("PlantShake", 1);
                                Player.GetComponent<EdgeCollider2D>().isTrigger = true;
                            }

                            // If player is in plant and wants to get out //
                            if (inPlant == true && goingOutPlant == false && waittoClick == false)
                            {
                                playerAnim.SetInteger("PlayerAnimation", 1);
                                goingOutPlant = true;
                            }
                        }
                    }
                }
            }
        }

        if (inPlant == true)
        {
            if (canshake == true)
            {
                plantAnim.SetInteger("PlantShake", 0);
                canshake = false;
            }

            if (canshake == false)
            {
                plantTimer = plantTimer + Time.deltaTime;

                if (plantTimer > 5)
                {
                    plantAnim.SetInteger("PlantShake", 1);
                }

                if (plantTimer > 7)
                {
                    canshake = true;
                    plantTimer = 0;
                }
            }
        }

        if (goingInPlant == true)
        {
            testtimer += Time.deltaTime;
            if (testtimer > 0.3)
            {

                
                playerInvis = true;
                Player.transform.position = Vector3.Lerp(Player.transform.position, foundPlantPos, jumpInOuttime * Time.deltaTime);

                if (Vector2.Distance(Player.transform.position, foundPlantPos) < playerPlantdistance)
                {
                    plantAnim.SetInteger("PlantShake", 0);
                    inPlant = true;

                    goingInPlant = false;
                    waittoClick = true;
                    testtimer = 0;
                }

            }
        }

        if (goingOutPlant == true)
        {
            testtimer += Time.deltaTime;
            if (testtimer > 0.4)
            {

                Player.transform.position = Vector3.Lerp(Player.transform.position, OldPlayerPos, jumpInOuttime * Time.deltaTime);

                if (Vector2.Distance(Player.transform.position, OldPlayerPos) < playerPlantdistance)
                {
                    inPlant = false;
                    Player.GetComponent<EdgeCollider2D>().isTrigger = false;
                    playerInvis = false;
                    canIMove.canMove = true;
                    goingOutPlant = false;
                    testtimer = 0;

                }
            }

        }

        if (waittoClick == true)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                waittoClick = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks the plant it's in area with and get objects & position //
        if (collision.gameObject.tag == "Plant")
        {
            selectedPlant = collision.gameObject;
            foundPlantPos = selectedPlant.transform.position;
            foundPlantPos = new Vector3(foundPlantPos.x + 0.99f, foundPlantPos.y + 2.19f, foundPlantPos.z);
            Debug.Log(foundPlantPos.y);
            plantHitCollider = selectedPlant.transform.GetChild(0).gameObject;
            foundPlant = true;
            plantAnim = selectedPlant.transform.parent.GetComponent<Animator>();

            if (inPlant == false)
            {
                selectedPlant.transform.parent.GetComponent<Animator>().enabled = false;
                selectedPlant.transform.parent.GetComponent<SpriteRenderer>().sprite = glowImg;
                selectedPlant.transform.parent.GetComponent<SpriteRenderer>().color = new Color32(222, 222, 222, 255);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If player isn't in the plant and gets out // 
        if (collision.gameObject.tag == "Plant" && inPlant == false)
        {
            foundPlant = false;

            if (inPlant == false)
            {
                selectedPlant.transform.parent.GetComponent<Animator>().enabled = true;
                selectedPlant.transform.parent.GetComponent<SpriteRenderer>().sprite = startImg;
                selectedPlant.transform.parent.GetComponent<SpriteRenderer>().color = new Color32(111, 111, 111, 255);
            }
        }
    }
}
