using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventoryButton : MonoBehaviour
{
    // Objects // 
    public GameObject outPull;
    public GameObject ButtonIcon;
    public GameObject inventoryManager;
    public GameObject inventoryHolder;


    // Touch //
    private Vector3 touchPos;

    // Positions //
    private Vector3 outPullStart;
    private Vector3 outPullEnd;

    // Bools //
    private bool pulledOut = false;

    private float pullSpeed = 5;

    public bool closingInv = false;

    public bool invIsOpen = false;


    void Start()
    {
        outPull = GameObject.Find("Inventaris_Label");
        ButtonIcon = GameObject.Find("InventarisButton");
        inventoryManager = GameObject.Find("InventoryManager");
        inventoryHolder = GameObject.Find("InventoryHolder");

    }


    void Update()
    {
        outPullStart = ButtonIcon.transform.position;
        outPullEnd = new Vector3((7.915f * this.transform.localScale.x) + ButtonIcon.transform.position.x, ButtonIcon.transform.position.y + 0.01f, ButtonIcon.transform.position.z);

        
        if (closingInv == false)
        {
            if (ButtonIcon.activeSelf == true)
            {
                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        touchPos = new Vector3(touchPos.x, touchPos.y, ButtonIcon.transform.position.z);

                        if (touch.phase == TouchPhase.Began)
                        {
                            if (pulledOut == false)
                            {
                                if (ButtonIcon.GetComponent<BoxCollider2D>().bounds.Contains(touchPos))
                                {
                                    pulledOut = true;
                                }

                                if (outPull.GetComponent<BoxCollider2D>().bounds.Contains(touchPos))
                                {
                                    pulledOut = true;
                                }
                            }
                            else
                            {
                                if (outPull.GetComponent<BoxCollider2D>().bounds.Contains(touchPos))
                                {
                                    pulledOut = false;

                                }

                                if (ButtonIcon.GetComponent<BoxCollider2D>().bounds.Contains(touchPos))
                                {
                                    pulledOut = false;
                                }
                            }
                        }
                    }
                }
            }

            if (pulledOut == true)
            {
                outPull.transform.position = Vector3.Lerp(outPull.transform.position, outPullEnd, pullSpeed * Time.deltaTime);

                if (Vector3.Distance(outPull.transform.position, outPullEnd) <= 1f)
                {
                    inventoryHolder.transform.parent.gameObject.active = true;
                    inventoryManager.transform.GetChild(3).GetComponent<SpriteMask>().enabled = true;
                    invIsOpen = true;
                }

            }

            if (pulledOut == false)
            {
                invIsOpen = false;
                inventoryHolder.transform.parent.gameObject.active = false;
                outPull.transform.position = Vector3.Lerp(outPull.transform.position, outPullStart, pullSpeed * Time.deltaTime);
                inventoryManager.transform.GetChild(3).GetComponent<SpriteMask>().enabled = false;
            }
        }

        if (closingInv == true)
        {
            pulledOut = false;
            outPull.transform.position = Vector3.Lerp(outPull.transform.position, outPullStart, pullSpeed * Time.deltaTime);
            inventoryManager.transform.GetChild(3).GetComponent<SpriteMask>().enabled = false;

            if (Vector3.Distance(outPull.transform.position, outPullStart) <= 0.5f)
            {
                closingInv = false;
                outPull.transform.position = outPullStart;
            }
        }
    }

}
