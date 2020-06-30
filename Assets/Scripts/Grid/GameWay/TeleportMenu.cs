
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool gotCaught = false;
    private string caughtText = "GuardSnap";
    private ProfTekstWay profT;

    private List<GameObject> safePoints = new List<GameObject>();
    private int whatSafePoint = 0;

    private PlayerAnimation playerAni;

    void Start()
    {
        playerAni = GameObject.Find("Player").GetComponent<PlayerAnimation>();
        profT = GameObject.Find("Canvas").gameObject.GetComponent<ProfTekstWay>();
        if (GameObject.Find("SpawnPoint") == true)
        {
            spawnPoint = GameObject.Find("SpawnPoint");
            safeAreas = GameObject.Find("SafeAreas");
            endPoint = GameObject.Find("EndPoint");
            Player = GameObject.Find("Player");

            safePoints.Add(spawnPoint);
            spawnPoint.GetComponent<SpriteRenderer>().enabled = false;
            endPoint.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < safeAreas.transform.childCount; i++)
            {
                var child = safeAreas.transform.GetChild(i).gameObject;
                child.GetComponent<SpriteRenderer>().enabled = false;
                safePoints.Add(child);
            }
        }
    }


    // Sets up player safe point when triggered with object // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Waypoint")
        {
            for (int i = 0; i < safePoints.Count; i++)
            {
                if (collision.name == safePoints[i].name)
                {
                    whatSafePoint = i;
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

        if(collision.gameObject == endPoint)
        {
            GameObject.Find("Player").GetComponent<Animator>().SetInteger("PlayerAnimation", 1);
            GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>().canMove = false;
            GameObject.Find("Canvas").GetComponent<SceneLoader>().loadScene = true;
        }
    }

    // Teleports player back to the last safe point // 
    public void teleportPlayer()
    {
        playerAni.caught = false;
        Player.GetComponent<Animator>().SetInteger("PlayerAnimation", 1);
        Player.transform.position = new Vector3(safePoints[whatSafePoint].transform.position.x + ((safePoints[whatSafePoint].transform.localScale.x / 2) * 1.98f), safePoints[whatSafePoint].transform.position.y + ((safePoints[whatSafePoint].transform.localScale.y / 2) * 1.46f), 0);
        GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>().canMove = true;
        GameObject.Find("Main Camera").transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y, -10);

        if (gotCaught == true)
        {
            profT.PutText(caughtText);
            gotCaught = false;
        }
    }



}
