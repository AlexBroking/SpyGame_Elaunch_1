using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SortingItems : MonoBehaviour
{

    void Start()
    {
        GameObject usedChild = this.gameObject;
        float childY = usedChild.transform.position.y;
        float childPositionY = childY + (usedChild.GetComponent<Collider2D>().offset.y) - 1.46f;
        int testint = Mathf.FloorToInt(10000 - (childPositionY / (1.46f / 3)));
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = (testint * 3) - 6;
    }
}
