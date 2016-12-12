using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clues : MonoBehaviour 
{
    public bool Motive;
    public bool Opportunity;
    public bool Means;

    public int Matches(Clues clues)
    {
        int matches = 0;
        matches += clues.Motive && this.Motive ? 1 : 0;
        matches += clues.Opportunity && this.Opportunity ? 1 : 0;
        matches += clues.Means && this.Means ? 1 : 0;
        return matches;
    }

    public void SetFromBoolArray(bool[] data)
    {
        Motive = data[0];
        Opportunity = data[1];
        Means = data[2];
    }

    public bool IsMurderer()
    {
        return Motive && Opportunity && Means;
    }
}
