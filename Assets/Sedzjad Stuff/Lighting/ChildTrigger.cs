using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<LightingPlafond>().PullTrigger(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<LightingPlafond>().LeavingArea(collision);
    }
}
