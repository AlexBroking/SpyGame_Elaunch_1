using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vragen
{
    public Ids[] vragen;
}

[System.Serializable]
public class Ids
{
    public string id;
    public string ProfTekst;
    public antwoorden[] A0;
    public antwoorden[] A1;
    public antwoorden[] A2;
}

[System.Serializable]
public class antwoorden
{
    public string Tekst;
    public string Answer;
}
