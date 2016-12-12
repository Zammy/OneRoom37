using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectiveClueState
{
    Positive,
    Negative,
    Locked,
    Unknown
}

public class Dossier
{
    public DetectiveClueState[] ClueStates;

    public Dossier ()
    {
        ClueStates = new DetectiveClueState[] { DetectiveClueState.Unknown, DetectiveClueState.Unknown, DetectiveClueState.Unknown };
    }

    //returns if chosing character as murderer
    public bool GetInfoFromClues(Clues clue)
    {
        var unknownIndexes = new List<int>();
        for (int i = 0; i < ClueStates.Length; i++)
        {
            if (ClueStates[i] == DetectiveClueState.Unknown)
            {
                unknownIndexes.Add(i);
            }
        }

        if (unknownIndexes.Count == 0)
        {
            return true;
        }

        int unknownSlotIndex = unknownIndexes.xRandomIndex();
        int index = unknownIndexes[unknownSlotIndex];
        unknownIndexes.RemoveAt(unknownSlotIndex);
        switch (index)
        {
            case 0:
            {
                ClueStates[0] = clue.Motive ? DetectiveClueState.Positive : DetectiveClueState.Negative;
                break;
            }
            case 1:
            {
                ClueStates[1] = clue.Opportunity ? DetectiveClueState.Positive : DetectiveClueState.Negative;
                break;
            }
            case 2:
            {
                ClueStates[2] = clue.Means ? DetectiveClueState.Positive : DetectiveClueState.Negative;
                break;
            }
            default:
                break;
        }

        if (unknownIndexes.Count == 1)
        {
            ClueStates[unknownIndexes[0]] = DetectiveClueState.Locked;
        }

        return false;
    }
}

public class DetectiveInteraction : BasePlayerInteraction
{
    Dictionary<GameObject, Dossier> dossiers = new Dictionary<GameObject,Dossier>();

    protected override void InteractWithNPC(GameObject interectWith)
    {
        var clues = interectWith.GetComponentInChildren<Clues>();
        Dossier dossier = GetDossierForCharacter(interectWith);
        bool gameEnd = dossier.GetInfoFromClues(clues);
        if (gameEnd)
        {
            WinState state = clues.IsMurderer() ? WinState.DetectiveFoundMurderer : WinState.DetectiveDidNotFoundMurderer;
            UIManager.Instance.ShowGameEnded(this.playerControls.PlayerNumber, state);
        }
        else
        {
            UIManager.Instance.ShowDetectiveInfo(interectWith.transform.position, dossier);
        }
    }

    Dossier GetDossierForCharacter(GameObject character)
    {
        Dossier dossier;
        if (!dossiers.TryGetValue(character, out dossier))
        {
            dossier = new Dossier(); 
            dossiers[character] = dossier;
        }
        return dossier;
    }
}
