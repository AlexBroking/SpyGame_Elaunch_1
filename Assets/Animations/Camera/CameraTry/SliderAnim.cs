using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnim : MonoBehaviour
{
    //Drag and drop your Slider into this variable.
    public Slider slider;
    private GameObject sliderObject;
    public Animator camera;
    public Animator light;
    public GameObject cam;
    // Use this for initialization

    void Start()
    {
        sliderObject = GameObject.Find("HeadSlider");
        slider = sliderObject.transform.GetChild(0).gameObject.GetComponent<Slider>();
        slider.value = 0;
        cam = this.gameObject;
        camera = cam.transform.GetChild(0).GetComponent<Animator>();
        light = cam.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        



    }
    void Update()
    {
        PlayAnimation();

    }

    public void PlayAnimation()
    {
        if (slider.value == 1)
        {
            slider.value = 0.99f;
        }

        if (sliderObject.active == false)
        {
            if (slider.value <= 0.5f)
            {
                slider.value = 0f;
            }

            if (slider.value >= 0.5f)
            {
                slider.value = 0.99f;
            }
        }

        camera.Play("CameraRotation", 0, slider.normalizedValue);
        light.Play("LightAnimation", 0, slider.normalizedValue);
    }

}
