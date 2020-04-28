using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    // Laser // 
    private GameObject laser;
    private Animator laserAni;
    public Animator buttonAniOne;
    public Animator buttonAniTwo;
    public bool laserOn;

    private float scale;

    // Outputs //  
    private Vector3 leftOutput;
    private Vector3 rightOutput;

    // timer //
    public int laserSeconds;
    private float timer;

    void Start()
    {
        leftOutput = this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).transform.position;
        rightOutput = this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).transform.position;

        laser = gameObject.transform.GetChild(4).gameObject;
        laserAni = laser.transform.GetComponent<Animator>();
        buttonAniOne = gameObject.transform.GetChild(0).GetComponent<Animator>();
        buttonAniOne.speed = 0;
        buttonAniTwo = gameObject.transform.GetChild(1).GetComponent<Animator>();
        buttonAniTwo.speed = 0;

        scale = Vector3.Distance(leftOutput, rightOutput);
        scale = scale / 1.98f;
        laser.transform.localScale = new Vector3(scale * 0.23f, 1, 1);


        laser.transform.GetComponent<SpriteRenderer>().enabled = true;
        laser.transform.GetComponent<BoxCollider2D>().enabled = true;
    }


    void Update()
    {
        laser.transform.position = new Vector3((leftOutput.x + rightOutput.x), (leftOutput.y + rightOutput.y), 0) / 2;

        if (laserOn == true)
        {
            laserAni.SetInteger("LaserAnimation", 0);
            
        }

        if (laserOn == false)
        {
            laserAni.SetInteger("LaserAnimation", 1);

            timer += Time.deltaTime;

            if (timer > laserSeconds)
            {
                laserOn = true;
                timer = 0;
                laser.transform.GetComponent<BoxCollider2D>().enabled = true;
                buttonAniOne.SetInteger("Button", 1);
                buttonAniTwo.SetInteger("Button", 1);
            }
        }
    }
}
