using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimation : MonoBehaviour
{
    private GameObject Guard;
    private Animator guardAni;
    private EnemyAI GuardC;
    private SpriteRenderer Gsprite;
    private GameObject guardLight;
    private GameObject Player;

    void Start()
    {
        Guard = this.gameObject;
        guardAni = Guard.GetComponent<Animator>();
        GuardC = Guard.GetComponent<EnemyAI>();
        Gsprite = Guard.GetComponent<SpriteRenderer>();
        guardLight = Guard.transform.GetChild(2).gameObject;
        Player = GameObject.Find("Player");
    }

    public IEnumerator TurntoPosition(float nowDeg, float goingDeg)
    {
        yield return new WaitForSeconds(1);

        if (nowDeg == 0)
        {
            if (goingDeg == 90)
            {
                guardAni.SetInteger("GuardAnim", 2);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(0.75f, 1.5f, 1);
                yield return new WaitForSeconds(1);
                Gsprite.flipX = false;
            }

            if (goingDeg == 270)
            {
                guardAni.SetInteger("GuardAnim", 1);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(0.75f, 1.5f, 1);
                yield return new WaitForSeconds(1);
                Gsprite.flipX = false;
            }
        }

        if (nowDeg == 90)
        {
            if (goingDeg == 0)
            {
                guardAni.SetInteger("GuardAnim", 3);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(1, 1.1f, 1);
                yield return new WaitForSeconds(1);
                Gsprite.flipX = false;
            }

            if (goingDeg == 180)
            {
                Gsprite.flipX = true;
                yield return new WaitForSeconds(1);
                guardAni.SetInteger("GuardAnim", 3);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(1, 1.1f, 1);

            }
        }

        if (nowDeg == 180)
        {

            if (goingDeg == 90)
            {
                guardAni.SetInteger("GuardAnim", 2);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(0.75f, 1.5f, 1);
                yield return new WaitForSeconds(1);
                Gsprite.flipX = false;
            }

            if (goingDeg == 270)
            {
                
                guardAni.SetInteger("GuardAnim", 1);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(0.75f, 1.5f, 1);
                yield return new WaitForSeconds(1);
                Gsprite.flipX = false;
            }
        }

        if (nowDeg == 270)
        {
            if (goingDeg == 0)
            {
                yield return new WaitForSeconds(1);
                guardAni.SetInteger("GuardAnim", 3);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(1, 1.1f, 1);
                Gsprite.flipX = false;
            }

            if (goingDeg == 180)
            {
                Gsprite.flipX = true;
                yield return new WaitForSeconds(1);
                guardAni.SetInteger("GuardAnim", 3);
                guardLight.transform.rotation = Quaternion.Euler(0, 0, goingDeg);
                guardLight.transform.localScale = new Vector3(1, 1.1f, 1);

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
            guardAni.SetBool("Walking", true);
        }

        if (GuardC.guardTurnDegrees == 90)
        {
            Gsprite.flipX = false;
            guardAni.SetBool("Walking", true);
        }

        if (GuardC.guardTurnDegrees == 180)
        {
            Gsprite.flipX = true;
            guardAni.SetBool("Walking", true);
        }

        if (GuardC.guardTurnDegrees == 270)
        {
            Gsprite.flipX = false;
            guardAni.SetBool("Walking", true);
        }
    }

    public IEnumerator CaughtPlayer()
    {
        

        if (GuardC.guardTurnDegrees == 0)
        {
            Gsprite.flipX = false;
            guardAni.SetBool("Caught", true);
        }

        if (GuardC.guardTurnDegrees == 90)
        {
            Gsprite.flipX = false;
            guardAni.SetBool("Caught", true);
        }

        if (GuardC.guardTurnDegrees == 180)
        {
            Gsprite.flipX = true;
            guardAni.SetBool("Caught", true);
        }

        if (GuardC.guardTurnDegrees == 270)
        {
            Gsprite.flipX = false;
            guardAni.SetBool("Caught", true);
        }

        yield return new WaitForSeconds(2);
        Player.GetComponent<TeleportMenu>().gotCaught = true;
        Player.GetComponent<TeleportMenu>().teleportPlayer();
        GuardC.caughtPlayer = false;
        GuardC.turning = false;
        guardAni.SetBool("Caught", false);
        GuardC.didAlreadyTurnToWalk = false;
        GuardC.doneCaught = false;

    }
}
