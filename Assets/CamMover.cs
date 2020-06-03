using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{
    public bool camOn = false;

    private GameObject closeButton;
    
    private Vector3 mousePos;

    void Start()
    {
        //closeButton = GameObject.Find
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);

                   // mousePos = new Vector3(mousePos.x, mousePos.y, foundObject.transform.position.z);

                    //if (foundObject.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                    //{

                    //}
                }
            }
        }

        if (camOn == true)
        {

        }



        if (camOn == false)
        {

        }
    }
}
