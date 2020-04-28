using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchController : MonoBehaviour
{
    public GameObject _MainCamera;

    public Transform _MainCameraPos;
    public Transform _SecurityCameraPos;


    public bool _CameraTransformed;
    // Start is called before the first frame update
    void Start()
    {
        _MainCamera.SetActive(true);
        _MainCameraPos = _MainCamera.GetComponent<Transform>();

        _CameraTransformed = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraSwitching();
    }

    public void CameraSwitching()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !_CameraTransformed)
        {
            _CameraTransformed = true;
            //_MainCameraPos.transform.position = _SecurityCameraPos.position;
            _MainCameraPos.transform.position = new Vector3(_SecurityCameraPos.position.x, _SecurityCameraPos.position.y, -10);
            Debug.Log(_SecurityCameraPos.position);

            return;
            
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _CameraTransformed)
        {
            _CameraTransformed = false;
            
            return;
        }
    }
}
