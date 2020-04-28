using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    // Laser // 
    private bool foundLaser = false;
    private GameObject laserObject;
    private GameObject laser;
    private Lasers thisLaser;

    // MousePos // 
    private Vector3 mousePos;


    void Update()
    {
        if (foundLaser == true)
        {
            if (thisLaser.laserOn == true) 
            {
                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {

                        if (touch.phase == TouchPhase.Began)
                        {
                            mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                            mousePos = new Vector3(mousePos.x, mousePos.y, laserObject.transform.position.z);

                            if (laserObject.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                            {
                                laser.transform.GetComponent<BoxCollider2D>().enabled = false;
                                thisLaser.laserOn = false;
                                thisLaser.buttonAniOne.SetInteger("Button", 0);
                                thisLaser.buttonAniOne.speed = 1;

                                thisLaser.buttonAniTwo.SetInteger("Button", 0);
                                thisLaser.buttonAniTwo.speed = 1;
                            }
                        }
                    }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player is close to the laserButton // 
        if (collision.gameObject.tag == "LaserTrigger")
        {
            foundLaser = true;
            laserObject = collision.gameObject.transform.parent.gameObject;
            laser = laserObject.transform.parent.transform.GetChild(4).gameObject;
            thisLaser = laserObject.transform.parent.GetComponent<Lasers>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LaserTrigger")
        { 
            foundLaser = false;
        }
    }
}
