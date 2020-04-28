using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Guard Enums // 
    public Guards chosenGuard;
    public StartPosition chosenTurn;
    public enum Guards { Flashlight_Walk, Flashligt_Stand, NoFlashlight_Walk, NoFlashlight_Stand, Sit_at_desk, Sleep }
    public enum StartPosition { Up, Down, Right, Left }
    private float[] degreeList = new float[] { 0, 90, 180, 270, 360 };

    // Guard Object // 
    private Animator guardAni;
    private GameObject Guard;
    private float startAniPosition;
    private float lookedAtPos;
    public float guardTurnDegrees;
    public bool turned = true;
    private bool canWalk = false;
    

    // Guard && Player // 
    private float guardToPlayerDegrees;
    private float oldGuardToPlayerDegrees;
    private int randomTurnInt;

    // Player // 
    private GameObject Player;
    private Vector3 heardPlayerPos;

    // Message // 
    public bool heardPlayer = false;

    // Scripts // 
    private GuardAnimation guardAniScript;

    private bool turning = false;
    private float wantToTurnToThis;
    public bool doneTurning = true;
    public List<Vector3> movePositions = new List<Vector3>();
    private int walkingInt = 1;
    private int maxWalk;
    public bool didAlreadyTurnToWalk = false;
    public float guardToWalkingPostionDegrees;
    private SpriteRenderer Gsprite;

    void Start()
    {
        Player = GameObject.Find("Player");
        Guard = this.gameObject;
        guardAni = Guard.GetComponent<Animator>();
        guardAniScript = Guard.GetComponent<GuardAnimation>();
        Gsprite = Guard.GetComponent<SpriteRenderer>();

        guardTurnDegrees = 0;

        // Check player startposition // 
        if (chosenTurn == StartPosition.Up) { TurnToWay(90); }
        if (chosenTurn == StartPosition.Down) { TurnToWay(270); }
        if (chosenTurn == StartPosition.Left) { TurnToWay(180); }


        movePositions.Add(this.gameObject.transform.position);
        for (int i = 0; i < this.gameObject.transform.GetChild(1).childCount; i++)
        {
            movePositions.Add(this.gameObject.transform.GetChild(1).GetChild(i).transform.position);
        }

        maxWalk = movePositions.Count;
    }

    void Update()
    {
        if (chosenGuard == Guards.Flashlight_Walk || chosenGuard == Guards.Flashligt_Stand || chosenGuard == Guards.NoFlashlight_Stand || chosenGuard == Guards.NoFlashlight_Walk)
        {
            if (doneTurning == true)
            {
                if (guardTurnDegrees != wantToTurnToThis)
                {
                    TurnToWay(wantToTurnToThis);
                }

                if (guardTurnDegrees == wantToTurnToThis)
                {
                    turning = false;
                }
            }

            if (turning == false)
            {
                DoingHisThing();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player has been found by the guard // 
        if (collision.gameObject.tag == "Player")
        {

            // If player walked in while guard was not looking for him yet // 
            if (heardPlayer == false)
            {
                heardPlayerPos = Player.transform.position;

                // Get the turn position the enemy has && check the player position //
                guardToPlayerDegrees = Mathf.Atan2(heardPlayerPos.y - Guard.transform.position.y, heardPlayerPos.x - Guard.transform.position.x) * (180 / Mathf.PI);
                if (heardPlayerPos.y < Guard.transform.position.y)
                {
                    guardToPlayerDegrees += 360;
                }
                oldGuardToPlayerDegrees = guardToPlayerDegrees;

                // Check what is closest degrees // 
                float shortestTest = 9999;
                for (int i = 0; i < degreeList.Length; i++)
                {
                    float shortestWay = degreeList[i] - guardToPlayerDegrees;
                    if (shortestWay < shortestTest && shortestWay > -45)
                    {
                        shortestTest = shortestWay;
                        guardToPlayerDegrees = degreeList[i];
                    }
                }
                if (guardToPlayerDegrees == 360)
                {
                    guardToPlayerDegrees = 0;
                }

                randomTurnInt = Random.Range(0, 2);
                heardPlayer = true;
            }
        }
    }

    private void TurnToWay(float turnToThis)
    {
        doneTurning = false;
        turning = true;
        wantToTurnToThis = turnToThis;

        // If looking right // 
        if (guardTurnDegrees == 0)
        {
            guardAni.SetInteger("GuardAnim", 3);
            Gsprite.flipX = false;
            if (turnToThis == 90) { StartCoroutine(guardAniScript.TurntoPosition(0, 90)); }
            if (turnToThis == 180)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(0, 90)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(0, 270)); }
            }
            if (turnToThis == 270) { StartCoroutine(guardAniScript.TurntoPosition(0, 270)); }
        }

        // If looking Up // 
        if (guardTurnDegrees == 90)
        {
            guardAni.SetInteger("GuardAnim", 2);
            Gsprite.flipX = false;
            if (turnToThis == 0) { StartCoroutine(guardAniScript.TurntoPosition(90, 0)); }
            if (turnToThis == 180) { StartCoroutine(guardAniScript.TurntoPosition(90, 180)); }
            if (turnToThis == 270)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(90, 180)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(90, 0)); }
            }
        }

        // If looking Left // 
        if (guardTurnDegrees == 180)
        {
            guardAni.SetInteger("GuardAnim", 3);
            Gsprite.flipX = true;
            if (turnToThis == 0)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(180, 90)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(180, 270)); }
            }
            if (turnToThis == 90) { StartCoroutine(guardAniScript.TurntoPosition(180, 90)); }
            if (turnToThis == 270) { StartCoroutine(guardAniScript.TurntoPosition(180, 270)); }
        }

        // If looking Down // 
        if (guardTurnDegrees == 270)
        {
            guardAni.SetInteger("GuardAnim", 1);
            Gsprite.flipX = false;
            if (turnToThis == 0) { StartCoroutine(guardAniScript.TurntoPosition(270, 0)); }
            if (turnToThis == 90)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(270, 180)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(270, 0)); }
            }
            if (turnToThis == 180) { StartCoroutine(guardAniScript.TurntoPosition(270, 180)); }
        }
    }



    private void DoingHisThing()
    {

        if (chosenGuard == Guards.Flashlight_Walk || chosenGuard == Guards.Flashligt_Stand || chosenGuard == Guards.NoFlashlight_Stand || chosenGuard == Guards.NoFlashlight_Walk)
        {
            if (didAlreadyTurnToWalk == false)
            {
                CheckWalkPos();
            }
            didAlreadyTurnToWalk = true;

            if (didAlreadyTurnToWalk == true && turning == false)
            {
                guardAniScript.MoveToPosition();
                transform.position = Vector3.MoveTowards(transform.position, movePositions[walkingInt], 5 * Time.deltaTime);
                if (transform.position == movePositions[walkingInt])
                {
                    walkingInt = walkingInt + 1;
                    if (walkingInt >= maxWalk)
                    {
                        walkingInt = 1;
                        movePositions.Reverse();
                    }
                    CheckWalkPos();

                }
            }

                
        }
    }

    private void CheckWalkPos()
    {
        
        guardToWalkingPostionDegrees = Mathf.Atan2(movePositions[walkingInt].y - Guard.transform.position.y, movePositions[walkingInt].x - Guard.transform.position.x) * (180 / Mathf.PI);

        if (movePositions[walkingInt].y < Guard.transform.position.y)
        {
            guardToWalkingPostionDegrees += 360;
        }
        oldGuardToPlayerDegrees = guardToWalkingPostionDegrees;

        // Check what is closest degrees // 
        float shortestTest = 9999;
        for (int i = 0; i < degreeList.Length; i++)
        {
            float shortestWay = degreeList[i] - guardToWalkingPostionDegrees;
            if (shortestWay < shortestTest && shortestWay > -45)
            {
                shortestTest = shortestWay;
                guardToWalkingPostionDegrees = degreeList[i];
            }
        }

        if (guardToWalkingPostionDegrees == 360)
        {
            guardToWalkingPostionDegrees = 0;
        }

        randomTurnInt = Random.Range(0, 2);

        if (guardToWalkingPostionDegrees != guardTurnDegrees)
        {
            TurnToWay(guardToWalkingPostionDegrees);
        }
    }
}
