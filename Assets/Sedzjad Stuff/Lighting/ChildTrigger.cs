using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    public bool _In =false;
    public void OnTriggerStay2D(Collider2D collision)
    {
        _In = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        _In = false;
    }
}
