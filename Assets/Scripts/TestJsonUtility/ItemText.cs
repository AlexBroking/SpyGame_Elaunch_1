using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemText
{
    public Iteminfo[] Items;
}

[System.Serializable]
public class Iteminfo
{
    public string id;
    public string Kind;
    public string Image;
    public string HeadTekst;
    public string Tekst;
}