using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnim : MonoBehaviour
{
    //Drag and drop your Slider into this variable.
    public Slider slider;
    public Animation anim;
    public GameObject cam;
    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Camera");
        anim = cam.transform.GetChild(0).GetComponent<Animation>();
        //Make sure you have attached your animation in the Animations attribute
        //anim.Play("CameraRotation");
        //anim["CameraRotation"].speed = 1;
    }
    void Update()
    {
        //anim["CameraRotation"].normalizedTime = slider.value;
    }
}
