using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : BasePlayerInteraction 
{   
    [SerializeField]
    Clues clues;

    protected override void InteractWithNPC(GameObject interectWith)
    {
        var otherClues = interectWith.GetComponentInChildren<Clues>();

        int matches = otherClues.Matches(this.clues);

        UIManager.Instance.ShowInfoFeedback(this.transform.position, interactableObjects[0].transform.position, matches);
    }
}