﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMenu : MonoBehaviour
{
    // 1 spawnpoint where the player will spawn
    // ?? safe areas where the player is safe
    // 1 endpoint where the player will end the game (do the quizz)

    // Player first point start at the spawnpoint. When the player is caught or dies it goes back to there
    // If the player reaches the first safe point the safe point will be removed and its the new spawnpoint
    // if the player dies. This will go on till the player reaches the endpoint.

    // If the player walks back to last safepoints it wont work. The furthest safepoints will be saved. Player
    // has to reach safepoints to actually get further in the game. Getting items etc.

    // If player has an item but actually hasn't reached an safe point for some reason the game ideas get shuffled
    // and the player can get info by something else. The player will lose the items he had when he gets caught.
       
    // In het UnderGameMen > Safeareas staan de safeareas in. Die moeten onder elkaar worden gezet op basis van
    // eerste naar laatste safe area.

    private GameObject spawnPoint;
    private GameObject safeAreas;
    private GameObject endPoint;
    private GameObject Player;

    private float playerFeetYPos;

    private List<GameObject> safePoints = new List<GameObject>();
    private int whatSafePoint = 0;

    private PlayerControllerMovement playerControl;

    void Start()
    {
        if (GameObject.Find("SpawnPoint") == true)
        {
            spawnPoint = GameObject.Find("SpawnPoint");
            safeAreas = GameObject.Find("SafeAreas");
            endPoint = GameObject.Find("EndPoint");
            Player = GameObject.Find("Player");
            playerControl = Player.GetComponent<PlayerControllerMovement>();

            playerFeetYPos = (Player.GetComponent<EdgeCollider2D>().offset.y) - (Player.GetComponent<EdgeCollider2D>().bounds.size.y / 2);

            safePoints.Add(spawnPoint);
            spawnPoint.GetComponent<SpriteRenderer>().enabled = false;
            endPoint.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < safeAreas.transform.childCount; i++)
            {
                var child = safeAreas.transform.GetChild(i).gameObject;
                child.GetComponent<SpriteRenderer>().enabled = false;
                safePoints.Add(child);
            }

            // Teleports back to last safepoint // 
            teleportPlayer();
        }
    }


    // Sets up player safe point when triggered with object // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Waypoint")
        {
            for (int i = 0; i < safePoints.Count; i++)
            {
                if (collision.GetInstanceID() == safePoints[i].GetInstanceID())
                {
                    whatSafePoint = i;
                }
            }
        }

        if(collision.gameObject == endPoint)
        {
            Debug.Log("reached the end");
        }
    }

    // Teleports player back to the last safe point // 
    public void teleportPlayer()
    {
        Player.transform.position = new Vector3(safePoints[whatSafePoint].transform.position.x + 1.98f, safePoints[whatSafePoint].transform.position.y + 3f, safePoints[whatSafePoint].transform.position.z);
    }



}
