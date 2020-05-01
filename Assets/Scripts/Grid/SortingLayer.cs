using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class SortingLayer : MonoBehaviour
{

    private GameObject playerObject;

    // Set different gameobject Layers
    private GameObject wallsLayer;
    private GameObject obstaclesLayer;
    private GameObject objectsLayer;

    // Set different layers for objects
    Dictionary<GameObject, float> wallsMenu = new Dictionary<GameObject, float>();
    Dictionary<GameObject, float> obstaclesMenu = new Dictionary<GameObject, float>();
    Dictionary<GameObject, float> objectsMenu = new Dictionary<GameObject, float>();


    private float playerY;


    void Start()
    {
        playerObject = GameObject.Find("Player");

        // Get given Layers for different objects
        wallsLayer = GameObject.Find("Walls");
        obstaclesLayer = GameObject.Find("Boxes");
        objectsLayer = GameObject.Find("Objects");

        // Walls // Object = Laag - Player = Laag
        for (int i = 0; i < wallsLayer.transform.childCount; i++)
        {
            if (wallsLayer.transform.GetChild(i).gameObject.tag == "Collider")
            {
                GameObject usedChild = wallsLayer.transform.GetChild(i).gameObject;
                float childY = usedChild.transform.position.y;
                float childPositionY = childY + (usedChild.GetComponent<Collider2D>().offset.y) - (usedChild.GetComponent<Collider2D>().bounds.size.y / 2);

                wallsMenu.Add(usedChild, childPositionY);
            }
        }

        // Obstacles // Object = Boven - Player = Laag  
        for (int i = 0; i < obstaclesLayer.transform.childCount; i++)
        {
            if (obstaclesLayer.transform.GetChild(i).gameObject.tag == "Collider")
            {
                GameObject usedChild = obstaclesLayer.transform.GetChild(i).gameObject;
                float childY = usedChild.transform.position.y;
                float childPositionY = childY + (usedChild.GetComponent<Collider2D>().offset.y) + (usedChild.GetComponent<Collider2D>().bounds.size.y / 2) - (usedChild.GetComponent<Collider2D>().bounds.size.y / 10);

                obstaclesMenu.Add(usedChild, childPositionY);
            }
        }

        // Objects // Object = Midden - Player = Midden
        for (int i = 0; i < objectsLayer.transform.childCount; i++)
        {
            if (objectsLayer.transform.GetChild(i).gameObject.tag == "Collider")
            {
                GameObject usedChild = objectsLayer.transform.GetChild(i).gameObject;
                float childY = usedChild.transform.position.y;
                float childPositionY = childY + (usedChild.GetComponent<Collider2D>().offset.y);
                float test = childY / 1.46f;

                objectsMenu.Add(usedChild, childPositionY);
            }
        }
    }


    void FixedUpdate()
    {
        playerY = playerObject.transform.position.y;

        // Walls // Object = Laag - Player = Laag
        WallLayer();
        // Obstacles // Object = Boven - Player = Laag   
        ObstacleLayer();
        // Objects // Object = Midden - Player = Midden
        ObjectLayer();
    }

    // Walls // Object = Laag - Player = Laag
    void WallLayer()
    {
        // Player feet position (Box Collider)
        float positionY = playerY  + (playerObject.GetComponent<EdgeCollider2D>().offset.y) - (playerObject.GetComponent<EdgeCollider2D>().bounds.size.y / 2);

        SetLayer(wallsMenu, positionY);
    }

    // Obstacles // Object = Boven - Player = Laag  
    void ObstacleLayer()
    {
        // Player feet position (Box Collider)
        float positionY = playerY  + (playerObject.GetComponent<EdgeCollider2D>().offset.y) - (playerObject.GetComponent<EdgeCollider2D>().bounds.size.y / 2) - (playerObject.GetComponent<EdgeCollider2D>().bounds.size.y / 10);

        SetLayer(obstaclesMenu, positionY);
    }

    // Objects // Object = Midden - Player = Midden
    void ObjectLayer()
    {
        // Player Middle Position (Box Collider)
        float positionY = playerY + (playerObject.GetComponent<EdgeCollider2D>().offset.y);

        SetLayer(objectsMenu, positionY);
    }

    void SetLayer(Dictionary<GameObject, float> dicO, float yPosition)
    {
        for (int j = 0; j < dicO.Count; j++)
        {
            if (dicO.Values.ElementAt(j) > yPosition)
            {
                dicO.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "BackObjectLayer";
            }
            if (dicO.Values.ElementAt(j) < yPosition)
            {
                dicO.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "FrontObjectLayer";
            }

        }
    }

}
