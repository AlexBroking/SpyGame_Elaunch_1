using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private enum Item { Blueprint, BlueCard, YellowCard, PinkCard, NoteOnWall, NoteOnTable, LaptopNormal, LaptopHacking };

    // Item //
    private PickupItem foundObjectScript;
    private GameObject foundItem;

    // Bools // 
    private bool found = false;
    public bool HackingNow = false;
    public bool PickupNow = false;

    // Touch // 
    private Vector3 touchPos;
    private GameObject inventoryHolder;
    private GameObject copyObject;

    private PlayerControllerMovement playerMove;

    void Start()
    {
        inventoryHolder = GameObject.Find("InventoryHolder");
        copyObject = GameObject.Find("InvObject");
        playerMove = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (found == true)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPos = new Vector3(touchPos.x, touchPos.y, foundItem.transform.position.z);

                    if (touch.phase == TouchPhase.Began)
                    {
                        if (foundItem.GetComponent<BoxCollider2D>().bounds.Contains(touchPos))
                        {
                            if (inventoryHolder.transform.childCount > 2)
                            {
                                inventoryHolder.GetComponent<RectTransform>().offsetMin = new Vector2(inventoryHolder.GetComponent<RectTransform>().offsetMin.x - 1f, inventoryHolder.GetComponent<RectTransform>().offsetMin.y);
                            }
                            
                            var newObject = Instantiate(copyObject);
                            newObject.transform.parent = inventoryHolder.transform;
                            newObject.name = foundObjectScript.tekstItem + "";
                            newObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = foundObjectScript.invItem;

                            playerMove.canMove = false;

                            if (foundObjectScript.hacking == false)
                            {
                                Destroy(foundItem);
                                PickupNow = true;
                            }

                            if (foundObjectScript.hacking == true)
                            {
                                foundItem.GetComponent<BoxCollider2D>().enabled = false;
                                foundItem.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                                HackingNow = true;
                            }

                            found = false;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            foundItem = collision.gameObject.transform.parent.gameObject;
            foundObjectScript = foundItem.GetComponent<PickupItem>();
            foundItem.GetComponent<SpriteRenderer>().sprite = foundObjectScript.glowImage;


            found = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            found = false;
            foundItem.GetComponent<SpriteRenderer>().sprite = foundObjectScript.NormalImage;
        }
    }
}
