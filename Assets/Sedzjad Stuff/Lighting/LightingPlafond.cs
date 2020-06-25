using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingPlafond : MonoBehaviour
{
    private GameObject _Player;
    public SpriteRenderer[] _SpriteR;
    public List<GameObject> lijstje;

    private float fadeSpeed;
    private float whatFade;


    private float _Alpha;

   
    void Start()
    {
        _Alpha = 255;
        _Player = GameObject.Find("Player");

        _SpriteR = gameObject.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).name == "Roof")
            {
                lijstje.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void Update()
    {

        foreach (GameObject _object in lijstje)
        {
            if (gameObject.transform.GetChild(0).GetComponent<ChildTrigger>()._In == true)
            {
                fadeSpeed = 3.5f;
                whatFade = 0;
            }

            if (gameObject.transform.GetChild(0).GetComponent<ChildTrigger>()._In == false)
            {
                fadeSpeed = 3.5f;
                whatFade = 255;
            }

            foreach (SpriteRenderer _Sprite in _SpriteR)
            {

                _Sprite.color = new Color32(0, 0, 0, (byte)_Alpha);
            }

        }


        _Alpha = Mathf.Lerp(_Alpha, whatFade, Time.deltaTime * fadeSpeed);
    }

}
