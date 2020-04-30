using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfTalkActivate : MonoBehaviour
{
    public string talkID;
    private ProfTekstWay profTekst;
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        profTekst = GameObject.Find("Canvas").GetComponent<ProfTekstWay>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        profTekst.PutText(talkID, this.gameObject);
    }
}
