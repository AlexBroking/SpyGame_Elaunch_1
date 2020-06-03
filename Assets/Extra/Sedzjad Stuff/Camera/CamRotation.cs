using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CamRotation : MonoBehaviour
{
    public GameObject _ColliderDown, _ColliderUp, _ColliderLeft, _ColliderRight;

    //private PlayCameraAnim _Cam;
    
    public bool _RotL, _RotR, _RotT, _RotD;

    private bool _RotEnabled, _ColDisabled;

    public SliderAnim _Slider;

    // Start is called before the first frame update
    void Start()
    {
        //_Cam = gameObject.GetComponentInParent<PlayCameraAnim>();
        DisableColliders();
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCollider();
    }

    private void DisableColliders()
    {
        _ColliderDown.SetActive(false);
        _ColliderUp.SetActive(false);
        _ColliderLeft.SetActive(false);
        _ColliderRight.SetActive(false);

        _ColDisabled = true;
        EnableColliders();
    }

    public void ChangeCollider()
    {
        if (_Slider.slider.IsActive() == true)
        {
            if (_Slider.slider.value <= 0.5f)
            {
                _ColliderRight.SetActive(false);
                _ColliderDown.SetActive(true);
                return;
            }
            if (_Slider.slider.value >= 0.51f)
            {
                _ColliderRight.SetActive(true);
                _ColliderDown.SetActive(false);
                return;
            }
        }
    }

    private void EnableColliders()
    {
        int _NumCol = 4;
        for(int i = 0; i < _NumCol; i++)
        {
            if (_ColDisabled)
            {
                if (_RotL)
                {
                    _ColliderLeft.SetActive(true);
                    _RotL = false;
                }
                if (_RotR)
                {
                    _ColliderRight.SetActive(true);
                    _RotR = false;
                }
                if (_RotT)
                {
                    _ColliderUp.SetActive(true);
                    _RotT = false;
                }
                if (_RotD)
                {
                    _ColliderDown.SetActive(true);
                    _RotD = false;
                }
            }
            
        }
        //ChangeColliders();
        
    }

}
