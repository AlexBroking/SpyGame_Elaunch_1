using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingBlocks : MonoBehaviour
{

    void Start()
    {
        GameObject usedChild = this.gameObject;
        float childY = usedChild.transform.position.y;
        float childPositionY = childY + (usedChild.GetComponent<Collider2D>().offset.y) - 1.46f;
        int testint = Mathf.FloorToInt(10000 - (childPositionY / (1.46f / 3)));
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (testint * 3) - 5;
    }
}
