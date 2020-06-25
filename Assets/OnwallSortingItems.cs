using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnwallSortingItems : MonoBehaviour
{
    void Start()
    {
        GameObject usedChild = this.gameObject.transform.gameObject;
        float childY = usedChild.transform.position.y;
        float childPositionY = childY + (usedChild.GetComponent<Collider2D>().offset.y) - (usedChild.GetComponent<Collider2D>().bounds.size.y / 2);
        int testint = Mathf.FloorToInt(10000 - (childPositionY / (1.46f / 3)));
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = (testint * 3) + 1;
    }

}
