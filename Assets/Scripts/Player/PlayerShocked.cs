using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShocked : MonoBehaviour
{
    private PlayerControllerMovement playerController;

    public bool isShocking = false;
    private float playerPosY;
    private float movingbacknumber;
    private float newPlayerPosY;
    private float touchedPosY;
    public bool aniWorking;
    private ProfTekstWay profT;
    private string laserHitText = "LaserHit";
    private bool afterHit;


    void Start()
    {
        playerController = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        profT = GameObject.Find("Canvas").gameObject.GetComponent<ProfTekstWay>();
    }

    private void Update()
    {
        if (isShocking == true)
        {
            if (aniWorking == true)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, newPlayerPosY + movingbacknumber, transform.position.z), 15 * Time.deltaTime);
            }
        }
        
        if (isShocking == false)
        {
            if (afterHit == true)
            {
                profT.PutText(laserHitText);
                afterHit = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Laser")
        {
            playerController.canMove = false;

            touchedPosY = collision.transform.position.y + (collision.gameObject.GetComponent<Collider2D>().offset.y);
            playerPosY = transform.position.y + (gameObject.GetComponent<EdgeCollider2D>().offset.y);
            newPlayerPosY = transform.position.y;

            // If laser will ever be like | and not only - // 
            // Check if the what for laser it is so it doesnt fck up with the Y pos// 

            // If player is under the laser the player falls downwards // 
            if (touchedPosY > playerPosY)
            {
                movingbacknumber = -1;
            }

            // If player is above the laser the player falls upwards // 
            if (playerPosY > touchedPosY)
            {
                movingbacknumber = 1;
            }

            isShocking = true;
            afterHit = true;
        }
    }
}
