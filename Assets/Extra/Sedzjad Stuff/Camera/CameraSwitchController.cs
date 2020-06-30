using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitchController : MonoBehaviour
{
    private GameObject _MainCamera;

    public Transform _SecurityCameraPos;
    public float camTransformSize;
    public GameObject closeButton;

    private GameObject controlCircle;
    private GameObject dashButton;
    private GameObject invButton;

    public bool _InComputer;

    private GameObject Player;

    private PlayerControllerMovement _playerMove;

    private GameObject _CCTVPanel;
    public GameObject foundObject;
 
    private Vector3 mousePos;
    private float startScale;

    void Start()
    {
        _CCTVPanel = GameObject.Find("HeadSlider");
        _playerMove = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        _MainCamera = GameObject.Find("Main Camera");
        closeButton = GameObject.Find("HeadSlider").transform.GetChild(1).gameObject;
        startScale = _MainCamera.GetComponent<Camera>().orthographicSize;

        Player = GameObject.Find("Player");

        controlCircle = GameObject.Find("Control_Circle");
        dashButton = GameObject.Find("DashButton");
        invButton = GameObject.Find("InventarisButton");

        _InComputer = false;
    }

    void Update()
    {
        CameraSwitching();
    }

    public void CameraSwitching()
    {
        if (_InComputer == true)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                        mousePos = new Vector3(mousePos.x, mousePos.y, closeButton.transform.position.z);

                        if (closeButton.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                        {
                            _InComputer = false;
                            _MainCamera.transform.localScale = new Vector3(1, 1, 1);
                            _MainCamera.GetComponent<Camera>().orthographicSize = startScale;
                            _playerMove.canMove = true;
                            foundObject.GetComponent<Animator>().enabled = false;
                            controlCircle.SetActive(true);
                            dashButton.SetActive(true);
                            invButton.SetActive(true);
                            _MainCamera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
                        }
                    }
                }
            }
        }

        if (_InComputer == true)
        {
            _playerMove.canMove = false;
            _MainCamera.transform.localScale = new Vector3(2, 2, 1);
            _MainCamera.GetComponent<Camera>().orthographicSize = startScale * 2;

            _SecurityCameraPos = GameObject.Find("CameraController").transform;
            _MainCamera.transform.position = new Vector3(_SecurityCameraPos.position.x, _SecurityCameraPos.position.y, -10);
            _CCTVPanel.SetActive(true);
            controlCircle.SetActive(false);
            dashButton.SetActive(false);
            invButton.SetActive(false);
        }

        if (_InComputer == false)
        {
            _CCTVPanel.SetActive(false);
        }
    }
}
