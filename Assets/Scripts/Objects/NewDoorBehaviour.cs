using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoorBehaviour : MonoBehaviour
{

    // Door // 
    public bool greenDoor;
    public bool PMBDoor;
    public string doorID;
    public string doorProfID;
    public bool sideDoor;
    public bool Qdoor;
    public bool Pdoor;
    public bool blackDoor;
    private GameObject blockedDoor;
    private bool hitQDoor = false;
    private bool hitPDoor = false;
    private bool hitPMDDoor = false;
    private bool onlyPdoor = false;
    private bool foundItem = false;


    // Animation // 
    private Animator doorAni;
    private Animator sideDoorExtra;

    // inventory // 
    private GameObject inventory;

    // Question //
    private Questions questionScript;

    // profTekst //
    private ProfTekstWay profTekst;



    void Start()
    {
        // Get InventoryManager // 
        inventory = GameObject.Find("InventoryHolder");
        questionScript = GameObject.Find("Canvas").GetComponent<Questions>();

        // Set DoorAnimation // 
        doorAni = this.gameObject.transform.parent.GetComponent<Animator>();
        doorAni.speed = 0;

        if (sideDoor == true)
        {
            sideDoorExtra = this.gameObject.transform.parent.GetChild(0).GetComponent<Animator>();
            sideDoorExtra.speed = 0;
        }

        // Get blocked Door Collider // 
        blockedDoor = this.gameObject.transform.parent.GetChild(2).gameObject;

        // Get proftekst //
        profTekst = GameObject.Find("Canvas").GetComponent<ProfTekstWay>();
    }

    private void Update()
    {
        if (blackDoor == true)
        {
            doorAni.speed = 1;
            doorAni.SetInteger("DoorBehaviour", 5);

            if (sideDoor == true)
            {
                sideDoorExtra.speed = 1;
                sideDoorExtra.SetInteger("DoorBehaviour", 5);
            }
        }

        if (hitQDoor == true)
        {
            profTekst.PutText(doorProfID);
            hitQDoor = false;
        }

        if (hitPDoor == true)
        {
            profTekst.openText = true;
            profTekst.PutText(doorProfID);
            onlyPdoor = true;
            hitPDoor = false;
        }

        if (profTekst.openText == false && onlyPdoor == true)
        {
            questionScript.OpenQuestion(doorID, this.gameObject);
            onlyPdoor = false;
        }

        if (hitPMDDoor == true)
        {
            profTekst.PutText(doorProfID);
            hitPMDDoor = false;
            
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the door is a Blue / Magenta / Orange //
        if (PMBDoor == true)
        {
            if (collision.transform.tag == "Player")
            {

                for (int i = 0; i < inventory.transform.childCount; i++)
                {
                    if (inventory.transform.GetChild(i).gameObject.name == doorID)
                    {
                        PMBDoor = false;
                        greenDoor = true;
                        foundItem = true;
                    }
                }

                if (foundItem == false)
                {
                    hitPMDDoor = true;
                }
            }
            if (inventory.transform.childCount == 0)
            {
                hitPMDDoor = true;
            }
        }

        if (Qdoor == true)
        {
            if (collision.transform.tag == "Player")
            {

                for (int i = 0; i < inventory.transform.childCount; i++)
                {
                    if (inventory.transform.GetChild(i).gameObject.name == doorID)
                    {
                        questionScript.OpenQuestion(doorID, this.gameObject);
                        foundItem = true;
                    }
                }

                if (foundItem == false)
                {
                    hitQDoor = true;
                }
            }
            if (inventory.transform.childCount == 0)
            {
                hitQDoor = true;
            }
        }



        if (Pdoor == true)
        {
            if (collision.transform.tag == "Player")
            {

                if (inventory.transform.childCount != 0)
                {
                    for (int i = 0; i < inventory.transform.childCount; i++)
                    {
                        if (inventory.transform.GetChild(i).gameObject.name == doorID)
                        {
                            questionScript.OpenQuestion(doorID, this.gameObject);
                            foundItem = true;
                        }
                    }

                    if (foundItem == false)
                    {
                        hitPDoor = true;
                    }
                }
                if (inventory.transform.childCount == 0)
                {
                    hitPDoor = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the door is already green and player can just walk in // 
        if (greenDoor == true)
        {
            if (collision.transform.tag == "Player")
            {
                doorAni.speed = 1;
                blockedDoor.GetComponent<BoxCollider2D>().enabled = true;
                doorAni.SetInteger("DoorBehaviour", 2);

                if (sideDoor == true)
                {
                    sideDoorExtra.speed = 1;
                    sideDoorExtra.SetInteger("DoorBehaviour", 2);
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the door is already green and player can just walk in //
        if (collision.transform.tag == "Player")
        {
            if (greenDoor == true)
            {
                doorAni.SetInteger("DoorBehaviour", 1);
                doorAni.speed = 1;
                blockedDoor.GetComponent<BoxCollider2D>().enabled = false;


                if (sideDoor == true)
                {
                    sideDoorExtra.SetInteger("DoorBehaviour", 1);
                    sideDoorExtra.speed = 1;
                }
            }
        }
    }
}
