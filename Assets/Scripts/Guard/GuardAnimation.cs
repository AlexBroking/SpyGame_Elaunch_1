﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimation : MonoBehaviour
{
    private GameObject Guard;
    private Animator guardAni;
    private PlayerControllerMovement playerC;
    private EnemyAI GuardC;
    private SpriteRenderer Gsprite;
    private BoxCollider2D guardTouch;
    private BoxCollider2D guardLight;

    void Start()
    {
        Guard = this.gameObject;
        guardAni = Guard.GetComponent<Animator>();
        GuardC = Guard.GetComponent<EnemyAI>();
        Gsprite = Guard.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    public IEnumerator TurntoPosition(float nowDeg, float goingDeg)
    {
        yield return new WaitForSeconds(1);

        if (nowDeg == 0)
        {
            if (goingDeg == 90)
            {
                guardAni.SetInteger("GuardAnim", 2);
                yield return new WaitForSeconds(2);
                Gsprite.flipX = false;
            }

            if (goingDeg == 270)
            {
                guardAni.SetInteger("GuardAnim", 1);
                yield return new WaitForSeconds(2);
                Gsprite.flipX = false;
            }
        }

        if (nowDeg == 90)
        {
            if (goingDeg == 0)
            {
                guardAni.SetInteger("GuardAnim", 3);
                yield return new WaitForSeconds(2);
                Gsprite.flipX = false;
            }

            if (goingDeg == 180)
            {
                Gsprite.flipX = true;
                yield return new WaitForSeconds(2);
                guardAni.SetInteger("GuardAnim", 3);
                
            }
        }

        if (nowDeg == 180)
        {

            if (goingDeg == 90)
            {
                guardAni.SetInteger("GuardAnim", 2);
                yield return new WaitForSeconds(2);
                Gsprite.flipX = false;
            }

            if (goingDeg == 270)
            {
                
                guardAni.SetInteger("GuardAnim", 1);
                yield return new WaitForSeconds(2);
                Gsprite.flipX = false;
            }
        }

        if (nowDeg == 270)
        {
            if (goingDeg == 0)
            {
                yield return new WaitForSeconds(2);
                guardAni.SetInteger("GuardAnim", 3);
                Gsprite.flipX = false;
            }

            if (goingDeg == 180)
            {
                Gsprite.flipX = true;
                yield return new WaitForSeconds(2);
                guardAni.SetInteger("GuardAnim", 3);
                
            }
        }

        GuardC.guardTurnDegrees = goingDeg;
        yield return new WaitForSeconds(1);
        GuardC.doneTurning = true;


    }

    public void MoveToPosition()
    {
        if (GuardC.guardTurnDegrees == 0)
        {
            Gsprite.flipX = false;
            guardAni.SetInteger("GuardAnim", 6);
        }

        if (GuardC.guardTurnDegrees == 90)
        {
            Gsprite.flipX = false;
            guardAni.SetInteger("GuardAnim", 5);
        }

        if (GuardC.guardTurnDegrees == 180)
        {
            Gsprite.flipX = true;
            guardAni.SetInteger("GuardAnim", 6);
        }

        if (GuardC.guardTurnDegrees == 270)
        {
            Gsprite.flipX = false;
            guardAni.SetInteger("GuardAnim", 7);
        }
    }
}
