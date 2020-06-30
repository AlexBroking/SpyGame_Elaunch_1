using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : MonoBehaviour
{
    public enum Way { GoingUp, GoingDown };
    public string startProfTekst;
    public Way thisWay;
    private Vector3 spawnPlace;
    private Vector3 moveTo;
    private Transform thisObjectT;
    private Animator thisAnim;
    private ProfTekstWay profTekst;
  

    private bool isWalking = false;

    private GameObject Player;
    private GameObject doorCollider;
    private Animator playerAni;
    private GameObject canvas;
    private GameObject mainCam;

    void Start()
    {
        thisObjectT = this.gameObject.transform;
        thisAnim = this.gameObject.GetComponent<Animator>();
        thisAnim.speed = 0;
        Player = GameObject.Find("Player");
        profTekst = GameObject.Find("Canvas").GetComponent<ProfTekstWay>();
        doorCollider = this.gameObject.transform.GetChild(0).gameObject;
        playerAni = GameObject.Find("Player").GetComponent<Animator>();
        canvas = GameObject.Find("Canvas");
        mainCam = canvas.transform.parent.gameObject;
        
        canvas.GetComponent<PlayerControllerMovement>().canMove = false;

        if (thisWay == Way.GoingUp)
        {
            playerAni.SetInteger("PlayerAnimation", 1);
            playerAni.SetFloat("PlayerDegrees", 0.25f);
            spawnPlace = new Vector3(thisObjectT.position.x + 1.98f, thisObjectT.position.y + 0f, thisObjectT.position.z);
            moveTo = new Vector3(thisObjectT.position.x + 1.98f, thisObjectT.position.y + 10.22f, thisObjectT.position.z);
        }

        if (thisWay == Way.GoingDown)
        {
            playerAni.SetInteger("PlayerAnimation", 1);
            playerAni.SetFloat("PlayerDegrees", 0.75f);
            spawnPlace = new Vector3(thisObjectT.position.x + 1.98f, thisObjectT.position.y + 2.19f, thisObjectT.position.z);
            moveTo = new Vector3(thisObjectT.position.x + 1.98f, thisObjectT.position.y - 2.19f, thisObjectT.position.z);
        }

        Player.transform.position = spawnPlace;

        StartCoroutine(OpenDoor());

    }

 
    void Update()
    {
        if (isWalking == true)
        {
            mainCam.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,-10);
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, moveTo, 3.5f * Time.deltaTime);

            if (thisWay == Way.GoingUp)
            {
                playerAni.SetInteger("PlayerAnimation", 2);
                playerAni.SetFloat("PlayerDegrees", 0.25f);
            }

            if (thisWay == Way.GoingDown)
            {
                playerAni.SetInteger("PlayerAnimation", 2);
                playerAni.SetFloat("PlayerDegrees", 0.75f);
            }

            if (Vector3.Distance(Player.transform.position, moveTo) < 0.8)
            {
                isWalking = false;
                playerAni.SetInteger("PlayerAnimation", 1);
                CloseDoor();

                if (thisWay == Way.GoingDown)
                {
                    playerAni.SetFloat("PlayerDegrees", 0.75f);
                }

                if (thisWay == Way.GoingUp)
                {
                    playerAni.SetFloat("PlayerDegrees", 0.75f);
                }
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        canvas.transform.position = new Vector3(canvas.transform.position.x, canvas.transform.parent.position.y + 16, canvas.transform.position.z);
        yield return new WaitForSeconds(2);
        thisAnim.speed = 1;
        yield return new WaitForSeconds(1);
        isWalking = true;
    }

    private void CloseDoor()
    {
        doorCollider.GetComponent<Collider2D>().enabled = true;
        thisAnim.SetInteger("DoorAnimation", 1);
        profTekst.PutText(startProfTekst);
        canvas.transform.position = new Vector3(canvas.transform.position.x, canvas.transform.parent.position.y, canvas.transform.position.z);
    }
}
