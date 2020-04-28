using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItemGridSnap : MonoBehaviour
{
    private float snapValueX = 1.98f;
    private float snapValueY = 1.46f;
    private float depth = 0;
    public bool thisObject;


    void Update()
    {
        if (thisObject == true)
        {
            float snapInverseX = 1 / snapValueX;
            float snapInverseY = 1 / snapValueY;

            float x, y, z;

            // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
            // so 1.45 to nearest .5 is 1.5
            x = Mathf.Round(this.gameObject.transform.position.x * snapInverseX) / snapInverseX;
            y = Mathf.Round(this.gameObject.transform.position.y * snapInverseY) / snapInverseY;
            z = depth;  // depth from camera

            this.gameObject.transform.position = new Vector3(x, y, z);
        }


        foreach (Transform test1 in transform)
        {
            float snapInverseX = 1 / snapValueX;
            float snapInverseY = 1 / snapValueY;

            float x, y, z;

            // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
            // so 1.45 to nearest .5 is 1.5
            x = Mathf.Round(test1.position.x * snapInverseX) / snapInverseX;
            y = Mathf.Round(test1.position.y * snapInverseY) / snapInverseY;
            z = depth;  // depth from camera

            test1.position = new Vector3(x, y, z);
        }

        if (Application.isPlaying)
        {
            this.gameObject.GetComponent<ItemGridSnap>().enabled = false;
        } else
        {
            this.gameObject.GetComponent<ItemGridSnap>().enabled = true;
        }
    }
}