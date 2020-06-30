using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    private GameObject startButton;
    private Vector3 mousePos;
    private bool clicked = false;

    private int scene;

    void Start()
    {
        startButton = this.gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (clicked == false)
        {
            this.gameObject.GetComponent<SceneLoader>().loadScene = true;
            clicked = true;
        }
    }
}
