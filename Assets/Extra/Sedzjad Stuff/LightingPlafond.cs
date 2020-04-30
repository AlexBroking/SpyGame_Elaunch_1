using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingPlafond : MonoBehaviour
{
    private GameObject _Player;

    public MeshRenderer[] _MeshR;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player");

        
        _MeshR = new MeshRenderer[gameObject.GetComponentsInChildren<MeshRenderer>().Length];
        _MeshR = gameObject.GetComponentsInChildren<MeshRenderer>();
    }


    public void PullTrigger(Collider2D collider)
    {
        if (_Player.GetComponent<EdgeCollider2D>())
        {
            
            foreach (MeshRenderer _Mesh in _MeshR)
            {

                _Mesh.enabled = false;

            }
        }
    }

    public void LeavingArea(Collider2D collider)
    {
        foreach (MeshRenderer _Mesh in _MeshR)
        {
            _Mesh.enabled = true;

        }
    }
}
