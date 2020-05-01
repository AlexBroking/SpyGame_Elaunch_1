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
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
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

        laser.transform.position = new Vector3((leftOutput.x + rightOutput.x), (leftOutput.y + rightOutput.y), 0) / 2;
        laser.GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.46f);
    }


    void Update()
    {
        float positionY = player.transform.position.y;
        positionY = positionY + (player.GetComponent<EdgeCollider2D>().offset.y);

        float laserPositionY = laser.transform.position.y;
        laserPositionY = laserPositionY + (laser.GetComponent<BoxCollider2D>().offset.y);

        if (laserPositionY > positionY)
        {
            laser.GetComponent<SpriteRenderer>().sortingLayerName = "BackObjectLayer";
            this.gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingLayerName = "BackObjectLayer";
            this.gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sortingLayerName = "BackObjectLayer";
        }

        if (laserPositionY < positionY)
        {

            laser.GetComponent<SpriteRenderer>().sortingLayerName = "FrontObjectLayer";
            this.gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingLayerName = "FrontObjectLayer";
            this.gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sortingLayerName = "FrontObjectLayer";
        }

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
