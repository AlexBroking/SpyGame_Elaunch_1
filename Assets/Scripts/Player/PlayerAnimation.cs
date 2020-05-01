using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAni;
    private GameObject Player;

    private PlayerControllerMovement playerControll;
    private SpriteRenderer sprite;
    private PlayerDashButton playerD;
    private PlayerShocked shocked;
    private PlayerItem playerPickup;
    private GameObject canvas;
    private HideInPlant hidePlant;
    private float degrees;
    private bool isPlayerMoving = false;
    private bool canPlayerMove = false;
    private bool isHeDashing = false;
    private bool isShocked = false;
    private bool pickUpItem = false;
    private bool hackingItem = false;
    private bool startAni = false;
    private bool inPlant = false;
    private bool inPlant2 = false;

    public AudioSource audioData;
    private bool canstep = true;

    void Start()
    {
        Player = GameObject.Find("Player");
        playerAni = Player.GetComponent<Animator>();
        playerControll = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        sprite = Player.GetComponent<SpriteRenderer>();
        playerD = GameObject.Find("Canvas").GetComponent<PlayerDashButton>();
        shocked = Player.GetComponent<PlayerShocked>();
        playerPickup = Player.GetComponent<PlayerItem>();
        canvas = GameObject.Find("Canvas");
        audioData = Player.GetComponent<AudioSource>();
        hidePlant = Player.GetComponent<HideInPlant>();

        playerAni.SetInteger("PlayerAnimation", 1);
    }

    IEnumerator WaitForStepping()
    {
        canstep = false;
        audioData.Play(0);
        yield return new WaitForSeconds(0.3f);
        canstep = true;
    }

    void Update()
    {
        isPlayerMoving = playerControll.Ismoving;
        degrees = playerControll.playerDegrees;
        canPlayerMove = playerControll.canMove;
        isHeDashing = playerD.dashTiming;
        isShocked = shocked.isShocking;
        pickUpItem = playerPickup.PickupNow;
        hackingItem = playerPickup.HackingNow;
        inPlant = hidePlant.goingInPlant;
        inPlant2 = hidePlant.inPlant;
        

        if (startAni == false)
        {
            canvas.SetActive(false);
            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Start_Rope_Animation") == false)
            {
                playerControll.canMove = true;
                startAni = true;
                canvas.SetActive(true);

            }
        }


        if (isPlayerMoving == true && canPlayerMove == true)
        {
            if (canstep == true)
            {
                StartCoroutine(WaitForStepping());
            }

            if (degrees >0  && degrees < 180)
            {
                if (isHeDashing == false)
                {
                    playerAni.SetInteger("PlayerAnimation", 4);
                } else
                {
                    playerAni.SetInteger("PlayerAnimation", 6);
                }

                if(degrees > 0 && degrees < 90)
                {
                    sprite.flipX = false;
                }

                if (degrees > 90 && degrees < 180)
                {
                    sprite.flipX = true;
                }
            }

            if (degrees < 360 && degrees > 180)
            {
                if (isHeDashing == false)
                {
                    playerAni.SetInteger("PlayerAnimation", 3);
                } else
                {
                    playerAni.SetInteger("PlayerAnimation", 7);
                }
                if (degrees < 360 && degrees > 270)
                {
                    sprite.flipX = false;
                }

                if (degrees < 270 && degrees > 180)
                {
                    sprite.flipX = true;
                }
            }
        } 

        if (isPlayerMoving == false && isShocked == false && canPlayerMove == true)
        {

            if (degrees > 0 && degrees < 180)
            {
                playerAni.SetInteger("PlayerAnimation", 2);
                if (degrees > 0 && degrees < 90)
                {
                    sprite.flipX = false;
                }

                if (degrees > 90 && degrees < 180)
                {
                    sprite.flipX = true;
                }
            }

            if (degrees < 360 && degrees > 180)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                if (degrees < 260 && degrees > 270)
                {
                    sprite.flipX = false;
                }

                if (degrees < 270 && degrees > 180)
                {
                    sprite.flipX = true;
                }
            }
        }

        if (isShocked == false && canPlayerMove == false && hackingItem == false && pickUpItem == false && inPlant == false && inPlant2 == false)
        {
            if (degrees > 0 && degrees < 180)
            {
                playerAni.SetInteger("PlayerAnimation", 2);
                if (degrees > 0 && degrees < 90)
                {
                    sprite.flipX = false;
                }

                if (degrees > 90 && degrees < 180)
                {
                    sprite.flipX = true;
                }
            }

            if (degrees < 360 && degrees > 180)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                if (degrees < 260 && degrees > 270)
                {
                    sprite.flipX = false;
                }

                if (degrees < 270 && degrees > 180)
                {
                    sprite.flipX = true;
                }
            }
        }
        

        if (canPlayerMove == false && isShocked == true && pickUpItem == false && hackingItem == false)
        {
            var thisAni = playerAni.GetInteger("PlayerAnimation");

            if (thisAni == 1 || thisAni == 3 || thisAni == 7)
            {
                playerAni.SetInteger("PlayerAnimation", 9);
            }

            if (thisAni == 2 || thisAni == 4 || thisAni == 6)
            {
                playerAni.SetInteger("PlayerAnimation", 8);
            }

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Shock_Down_Animation_9") == true || playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Shock_Up_Animation_8") == true)
            {

                shocked.aniWorking = true;
            }

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Up_Animation_2") == true || playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Down_Animation_1") == true)
            {

                shocked.aniWorking = false;
                shocked.isShocking = false;
                playerControll.canMove = true;
            }
        }

        if (canPlayerMove == false && pickUpItem == true)
        {
            if (degrees > 0 && degrees < 180)
            {
                playerAni.SetInteger("PlayerAnimation", 12);
            }

            if (degrees < 360 && degrees > 180)
            {
                playerAni.SetInteger("PlayerAnimation", 11);
            }

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Down_Animation_1") == true || playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Up_Animation_2") == true)
            {
                playerControll.canMove = true;
                playerPickup.PickupNow = false;
            }

        }

        if (canPlayerMove == false && hackingItem == true)
        {
            playerAni.SetInteger("PlayerAnimation", 13);

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Hack_Animation") == true)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
            }

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Down_Animation_1") == true)
            {
                playerControll.canMove = true;
                playerPickup.HackingNow = false;
                hackingItem = false;
            }
        }
    }
}
