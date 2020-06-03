using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Camera //
    private GameObject Camera;
    private float maxCamDistane = 4;
    public float camSpeed = 2.5f;
    private Vector3 newCamPos;

    // Player // 
    private GameObject Player;
    private Vector3 PlayerPos;
    private bool playerIsWalking = false;
    private PlayerControllerMovement playerC;
    private bool didMoveCam = false;
    

    void Start()
    {
        // Get Camera & Player Objects
        Camera = this.gameObject;
        Player = GameObject.Find("Player");
        playerC = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
    }


    void Update()
    {
        PlayerPos = Player.transform.position;
        playerIsWalking = playerC.Ismoving;

        if (playerC.canMove == true)
        {
            didMoveCam = false;
        }

        if (playerC.canMove == false && didMoveCam == false)
        {
            // It's on the player or a random video will be played so the cam has to move to somewhere else //
            Camera.transform.position = new Vector3(PlayerPos.x, PlayerPos.y, -10);
            didMoveCam = true;
        }

        if (playerIsWalking == true)
        {
            float newdistance = maxCamDistane / 2;

            float dist = Vector3.Distance(playerC.smallerPos, playerC.biggerPos);
            float seconddist = Vector3.Distance(Camera.transform.position, PlayerPos + new Vector3(playerC.smallerPos.x - playerC.biggerPos.x, playerC.smallerPos.y - playerC.biggerPos.y).normalized * maxCamDistane);
            float thirddist = Vector3.Distance(PlayerPos + new Vector3(playerC.smallerPos.x - playerC.biggerPos.x, playerC.smallerPos.y - playerC.biggerPos.y).normalized * newdistance, Camera.transform.position);

            newCamPos = Vector3.Lerp(Camera.transform.position, PlayerPos + new Vector3(playerC.smallerPos.x - playerC.biggerPos.x, playerC.smallerPos.y - playerC.biggerPos.y).normalized * maxCamDistane, camSpeed * Time.deltaTime);
            newCamPos = new Vector3(newCamPos.x, newCamPos.y, -10);
            Camera.transform.position = newCamPos;
        }
    }
}