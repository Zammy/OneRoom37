using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : BaseInteraction 
{   
    [SerializeField]
    Clues clues;

    protected override void OnButtonPressed(InputButton button)
    {
        if (button != InputButton.Interact)
        {
            return;
        }

        if (playersInfront.Count == 0)
        {
            return;
        }

        var interactingWith = playersInfront[0];
        var otherClues = interactingWith.GetComponentInChildren<Clues>();

        int matches = otherClues.Matches(this.clues);

        UIManager.Instance.ShowInfoFeedback(this.transform.position, playersInfront[0].transform.position, matches);
    }
}