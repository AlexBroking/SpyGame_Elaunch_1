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
    private ItemsDataScript itemData;
    private float degrees;
    private bool isPlayerMoving = false;
    private bool canPlayerMove = false;
    private bool isHeDashing = false;
    private bool isShocked = false;
    private bool pickUpItem = false;
    private bool hackingItem = false;
    public bool caught = false;
    private bool waitForAnim = false;

    public AudioSource audioData;
    private bool canstep = true;

    private float playerY;

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
        itemData = canvas.GetComponent<ItemsDataScript>();
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
        playerY = Player.transform.position.y;
        float positionY = playerY + (Player.GetComponent<EdgeCollider2D>().offset.y) - (Player.GetComponent<EdgeCollider2D>().bounds.size.y / 2);
        int testint = Mathf.FloorToInt(10000 - (positionY / (1.46f / 3)));
        Player.GetComponent<SpriteRenderer>().sortingOrder = (testint * 3) + 3;



        isPlayerMoving = playerControll.Ismoving;
        degrees = playerControll.playerDegrees;
        canPlayerMove = playerControll.canMove;
        isHeDashing = playerD.dashTiming;
        isShocked = shocked.isShocking;
        pickUpItem = playerPickup.PickupNow;
        hackingItem = playerPickup.HackingNow;



        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Walk") == true)
        {
            if (canstep == true)
            {
                StartCoroutine(WaitForStepping());
            }
        }

        

        // Check player degrees for flip // 
        if (degrees > 0 && degrees < 180)
        {
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
            if (degrees < 360 && degrees > 270)
            {
                sprite.flipX = false;
            }

            if (degrees < 270 && degrees > 180)
            {
                sprite.flipX = true;
            }
        }

        // Moving & Dashing //
        if (isPlayerMoving == true && canPlayerMove == true)
        {
            playerAni.SetFloat("PlayerDegrees", degrees / 360);
            if (isHeDashing == false)
            {
                playerAni.SetInteger("PlayerAnimation", 2);
            }
            else
            {
                playerAni.SetInteger("PlayerAnimation", 3);
            }
        } 

        if (isPlayerMoving == false && canPlayerMove == true)
        {
            playerAni.SetInteger("PlayerAnimation", 1);
        }

        if (canPlayerMove == false && isShocked == true)
        {
            playerAni.SetInteger("PlayerAnimation", 4);
            Vector3 initialPosition = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,-10);
            canvas.transform.parent.position = Vector3.Lerp(canvas.transform.parent.position, Player.transform.position, Time.deltaTime * 10);

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Shock") == true)
            {
                shocked.aniWorking = true;
            }

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Empty") == true && shocked.aniWorking == true)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                shocked.aniWorking = false;
                shocked.isShocking = false;
                canvas.transform.parent.transform.localPosition = initialPosition;
            }
        }

        if (canPlayerMove == false && pickUpItem == true)
        {

            playerAni.SetInteger("PlayerAnimation", 5);

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Pickup") == true)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                waitForAnim = true;
            }

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Empty") == true && waitForAnim == true)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                waitForAnim = false;
                playerControll.canMove = true;
                playerPickup.PickupNow = false;

                if (playerPickup.checkItem == true)
                {
                    itemData.OpenTab(playerPickup.inventoryHolder.transform.GetChild(playerPickup.inventoryHolder.transform.childCount - 1).name);
                    playerPickup.checkItem = false;
                }
            }

        }

        if (canPlayerMove == false && hackingItem == true)
        {
            playerAni.SetInteger("PlayerAnimation", 6);

            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Hack") == true)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                waitForAnim = true;
            }


            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Empty") == true && waitForAnim == true)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                waitForAnim = false;
                playerControll.canMove = true;
                playerPickup.HackingNow = false;
                hackingItem = false;

                if (playerPickup.checkItem == true)
                {
                    itemData.OpenTab(playerPickup.inventoryHolder.transform.GetChild(playerPickup.inventoryHolder.transform.childCount - 1).name);
                    playerPickup.checkItem = false;
                }
            }
        }

        if (caught == true)
        {
            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Caught") == false)
            {
                playerAni.SetInteger("PlayerAnimation", 10);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            if (hidePlant.playerInvis == false)
            {
                hidePlant.foundPlant = false;

                playerControll.canMove = false;
                caught = true;

                if (collision.gameObject.name == "Light")
                {
                    collision.transform.parent.parent.GetComponent<EnemyAI>().caughtPlayer = true;
                }

                if (collision.gameObject.name == "CatchedCircle")
                {
                    collision.transform.parent.GetComponent<EnemyAI>().caughtPlayer = true;
                }
            }
        }

        if (collision.gameObject.tag == "CameraLight")
        {
            StartCoroutine(GotCaughtByCamera());
        }
    }

    private IEnumerator GotCaughtByCamera()
    {

        playerControll.canMove = false;
        caught = true;
        yield return new WaitForSeconds(2f);
        Player.GetComponent<TeleportMenu>().gotCaught = true;
        Player.GetComponent<TeleportMenu>().teleportPlayer();
    }

}
