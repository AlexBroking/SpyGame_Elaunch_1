using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditModeGridSnap : MonoBehaviour
{

    private float snapValueX = 1.98f;
    private float snapValueY = 1.46f;
    private float depth = 0;

    
    void Update()
    {
        foreach (Transform test1 in transform)
        {
            foreach (Transform test2 in test1)
            {
                float snapInverseX = 1 / snapValueX;
                float snapInverseY = 1 / snapValueY;

                float x, y, z;

                // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
                // so 1.45 to nearest .5 is 1.5
                x = Mathf.Round(test2.position.x * snapInverseX) / snapInverseX;
                y = Mathf.Round(test2.position.y * snapInverseY) / snapInverseY;
                z = depth;  // depth from camera

                test2.position = new Vector3(x, y, z);

                if (test2.childCount > 0)
                {
                    foreach (Transform test3 in test2)
                    {
                        float snapInverseX2 = 1 / snapValueX;
                        float snapInverseY2 = 1 / snapValueY;

                        float x2, y2, z2;

                        // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
                        // so 1.45 to nearest .5 is 1.5
                        x2 = Mathf.Round(test3.position.x * snapInverseX2) / snapInverseX2;
                        y2 = Mathf.Round(test3.position.y * snapInverseY2) / snapInverseY2;
                        z2 = depth;  // depth from camera

                        test2.position = new Vector3(x2, y2, z2);

                        if (test3.childCount > 0)
                        {
                            foreach (Transform test4 in test3)
                            {
                                float snapInverseX3 = 1 / snapValueX;
                                float snapInverseY3 = 1 / snapValueY;

                                float x3, y3, z3;

                                // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
                                // so 1.45 to nearest .5 is 1.5
                                x3 = Mathf.Round(test4.position.x * snapInverseX3) / snapInverseX3;
                                y3 = Mathf.Round(test4.position.y * snapInverseY3) / snapInverseY3;
                                z3 = depth;  // depth from camera

                                if (test4.childCount > 0)
                                {
                                    foreach (Transform test5 in test4)
                                    {
                                        float snapInverseX4 = 1 / snapValueX;
                                        float snapInverseY4 = 1 / snapValueY;

                                        float x4, y4, z4;

                                        // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
                                        // so 1.45 to nearest .5 is 1.5
                                        x4 = Mathf.Round(test5.position.x * snapInverseX4) / snapInverseX4;
                                        y4 = Mathf.Round(test5.position.y * snapInverseY4) / snapInverseY4;
                                        z4 = depth;  // depth from camera
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
        }

        if (Application.isPlaying)
        {
            this.gameObject.GetComponent<EditModeGridSnap>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<EditModeGridSnap>().enabled = true;
        }
    }
}