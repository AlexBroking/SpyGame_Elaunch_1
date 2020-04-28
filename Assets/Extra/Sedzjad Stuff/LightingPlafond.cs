using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingPlafond : MonoBehaviour
{
    public GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_Player.GetComponent<EdgeCollider2D>())
        {
            gameObject.SetActive(false);
        }
    }
}
