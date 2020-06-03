using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimationController : MonoBehaviour
{
    private Animator _Animator;

    private const string _CameraAnimation_Bool = "CameraAnimation";
    private const string _ReverseCameraAnimation_Bool = "ReverseCameraAnimation";



    private void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        Animate(_CameraAnimation_Bool);
    }
    public void ReversePlayAnimation()
    {
        Animate(_ReverseCameraAnimation_Bool);
    }

    private void Animate(string _BoolName)
    {
        DisableAnimations(_Animator, _BoolName);

        _Animator.SetBool(_BoolName, true);
    }

    private void DisableAnimations(Animator _Ani, string _Animation)
    {
        foreach(AnimatorControllerParameter parameter in _Animator.parameters)
        {
            if(parameter.name != _Animation)
            {
                _Animator.SetBool(parameter.name, false);
            }
        }
    }
}
