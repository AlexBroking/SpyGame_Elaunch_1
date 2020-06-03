using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingItems : MonoBehaviour
{
    private float playerY;
    private GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    void Update()
    {
        playerY = playerObject.transform.position.y;
        float childY = this.gameObject.transform.position.y;

        float childPositionY = childY + (this.gameObject.GetComponent<Collider2D>().offset.y);

        float positionY = playerY + (playerObject.GetComponent<EdgeCollider2D>().offset.y);

        if (positionY < childPositionY)
        {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "BackObjectLayer";
        }

        if (positionY > childPositionY)
        {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "FrontObjectLayer";
        }
    }
}
