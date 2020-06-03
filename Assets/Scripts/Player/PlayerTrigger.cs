using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    // Both // 
    private GameObject foundObject;

    // Laser // 
    private bool foundLaser = false;
    private GameObject laser;
    private Lasers thisLaser;
    public Sprite glowLaserImg;
    public Sprite normLaserImg;

    // Camera //
    private bool foundCam = false;
    private bool hasClickedCam = false;
    private CameraSwitchController thisCam;
    public Sprite glowCamImg;
    public Sprite normCamImg;

    // Player //

    // MousePos // 
    private Vector3 mousePos;

    private void Start()
    {
        thisCam = GameObject.Find("Canvas").gameObject.GetComponent<CameraSwitchController>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    

                    if (foundLaser == true)
                    {
                        if (thisLaser.laserOn == true)
                        {
                            mousePos = new Vector3(mousePos.x, mousePos.y, foundObject.transform.position.z); 

                            if (foundObject.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                            {

                                foundObject.GetComponent<Animator>().enabled = true;
                                laser.transform.GetComponent<BoxCollider2D>().enabled = false;
                                thisLaser.laserOn = false;
                                thisLaser.buttonAniOne.SetInteger("Button", 0);
                                thisLaser.buttonAniOne.speed = 1;

                                thisLaser.buttonAniTwo.SetInteger("Button", 0);
                                thisLaser.buttonAniTwo.speed = 1;
                            }
                        }
                    }

                    if (foundCam == true)
                    {
                        mousePos = new Vector3(mousePos.x, mousePos.y, foundObject.transform.position.z);

                        if (foundObject.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                        {
                            hasClickedCam = true;
                            foundObject.GetComponent<Animator>().enabled = true;

                            foundObject.GetComponent<Animator>().SetInteger("Button", 0);
                            foundObject.GetComponent<Animator>().speed = 1;

                            thisCam._InComputer = true;
                            thisCam.foundObject = foundObject;
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
            foundObject = collision.gameObject.transform.parent.gameObject;
            laser = foundObject.transform.parent.transform.GetChild(4).gameObject;
            thisLaser = foundObject.transform.parent.GetComponent<Lasers>();
            foundObject.GetComponent<SpriteRenderer>().color = new Color32(222, 222, 222, 255);

            if (thisLaser.laserOn == true)
            {
                foundObject.GetComponent<Animator>().enabled = false;
                foundObject.GetComponent<SpriteRenderer>().sprite = glowLaserImg;
            }
        }

        if (collision.gameObject.tag == "CameraTrigger")
        {
            foundCam = true;
            foundObject = collision.gameObject.transform.parent.gameObject;

            if (hasClickedCam == false)
            {
                foundObject.GetComponent<SpriteRenderer>().color = new Color32(222, 222, 222, 255);
                foundObject.GetComponent<SpriteRenderer>().sprite = glowCamImg;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LaserTrigger")
        { 
            foundLaser = false;
            foundObject.GetComponent<SpriteRenderer>().color = new Color32(111, 111, 111, 255);

            if (thisLaser.laserOn == true)
            {
                foundObject.GetComponent<Animator>().enabled = true;
                foundObject.GetComponent<SpriteRenderer>().sprite = normLaserImg;
                
            }
        }

        if (collision.gameObject.tag == "CameraTrigger")
        {
            foundCam = false;
            hasClickedCam = false;
            foundObject.GetComponent<SpriteRenderer>().sprite = normCamImg;
            foundObject.GetComponent<SpriteRenderer>().color = new Color32(111, 111, 111, 255);
        }


    }
}
