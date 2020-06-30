using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashButton : MonoBehaviour
{
    // Als je dashed mag je alle kanten op gaan // 
    // Als je dashed moet je bij de "PlayerControllerMovement" script kijken of je wel of niet mag dashen // 

    // Player // 
    private bool canMove = true;
    private PlayerControllerMovement playerController;

    // Camera //
    private float camDashSpeed;
    private float normalCamSpeed;
    private FollowPlayer camP;

    // Touch //
    private Vector3 touchPos;

    // Dashing //
    private GameObject dashButton;
    private bool canIDash = false;
    private bool isDashing = false;
    private float dashSpeed;
    private float normalspeed;
    public bool dashTiming = false;

    private AudioSource audioData;

    void Start()
    {
        dashButton = GameObject.Find("DashButton");
        playerController = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        camP = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
        camDashSpeed = camP.camSpeed * 2;
        normalCamSpeed = camP.camSpeed;
        dashSpeed = playerController.playerSpeed * 2;
        normalspeed = playerController.playerSpeed;
        audioData = dashButton.GetComponent<AudioSource>();
    }

    void Update()
    {

        canMove = playerController.canMove;

        if (canMove == true)
        {
            canIDash = playerController.Ismoving;

            if (canIDash == true)
            {
                if (isDashing == false)
                {
                    // Check for input & if player can dash (needs to move) //
                    if (Input.touchCount > 0)
                    {
                        foreach (Touch touch in Input.touches)
                        {

                            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPos = new Vector3(touchPos.x, touchPos.y, dashButton.transform.position.z);

                            if (touch.phase == TouchPhase.Began)
                            {
                                if (dashButton.GetComponent<BoxCollider2D>().bounds.Contains(touchPos))
                                {
                                    StartCoroutine(Dash());
                                }
                            }
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        StartCoroutine(Dash());
                    }
                }
            }
        } 
    }

    private IEnumerator Dash()
    {
        audioData.Play(0);
        playerController.playerSpeed = dashSpeed;
        camP.camSpeed = camDashSpeed;
        dashTiming = true;
        isDashing = true;
        yield return new WaitForSeconds(0.4f);
        dashTiming = false;
        playerController.playerSpeed = normalspeed;
        camP.camSpeed = normalCamSpeed;
        yield return new WaitForSeconds(2f);
        isDashing = false;
    }
}
