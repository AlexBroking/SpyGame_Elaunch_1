using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    
    public enum Item { Blueprint, BlueCard, YellowCard, PinkCard, NoteOnWall, NoteOnTable, LaptopHacking};
    public Item thisItem;
    public Sprite NormalImage;
    public Sprite glowImage;
    public Sprite invItem;
    

    public string tekstItem;
    public string pTekst;
    public bool hacking;

    private void Start()
    {
        NormalImage = this.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
