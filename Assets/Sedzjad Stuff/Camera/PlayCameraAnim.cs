using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCameraAnim : MonoBehaviour
{
    private GameObject _Cam;
    private CamRotation _CamRot;

    public GameObject _ArrowRight;
    public GameObject _ArrowLeft;

    public bool _BZ, _ZB;

    private Vector3 mousePos;

    private void Start()
    {
        _Cam = gameObject;
        _CamRot = gameObject.GetComponentInChildren<CamRotation>();
    }

    public void AnimController()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {

                if (touch.phase == TouchPhase.Began)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    mousePos = new Vector3(mousePos.x, mousePos.y, _ArrowLeft.transform.position.z);

                    if (_ArrowLeft.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                    {
                        if (_BZ)
                        {
                            PlayNormalAnimation();
                            _BZ = false;
                            _ZB = true;

                            return;
                        }
                        //_CamRot.ChangeColliders();
                    }
                    if (_ArrowRight.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                    {
                        if (_ZB)
                        {
                            PlayReversedAnimation();
                            _ZB = false;
                            _BZ = true;

                            return;
                        }
                        //_CamRot.ChangeColliders();
                    }
                    
                }
            }
        }
        
    }

    public void PlayNormalAnimation()
    {
        _Cam.GetComponent<Animation>().Play("CamRotation");
        _Cam.GetComponentInChildren<Animation>().Play("CamLightRotation");
    }

    public void PlayReversedAnimation()
    {
        _Cam.GetComponent<Animation>().Play("ReversedCamRotation");
        _Cam.GetComponentInChildren<Animation>().Play("ReversedCamLightRotation");
    }


}