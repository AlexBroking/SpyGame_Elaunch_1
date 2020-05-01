using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnim : MonoBehaviour
{
    //Drag and drop your Slider into this variable.
    public Slider slider;
    public Animator camera;
    public Animator light;
    public GameObject cam;
    // Use this for initialization
    void Start()
    {
        cam = this.gameObject;
        camera = cam.transform.GetChild(0).GetComponent<Animator>();
        light = cam.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        camera.Play("CameraRotation", 0, slider.normalizedValue);
        light.Play("LightAnimation", 0, slider.normalizedValue);
    }
}
