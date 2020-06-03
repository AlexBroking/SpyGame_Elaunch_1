using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfTekst
{
    public ProfInfo[] Items;
}

[System.Serializable]
public class ProfInfo
{
    public string id;
    public allTekst[] TekstArray;
}

[System.Serializable]
public class allTekst
{
    public string Name;
    public string tekst;
}