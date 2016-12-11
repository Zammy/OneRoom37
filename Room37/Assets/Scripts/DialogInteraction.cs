using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : MonoBehaviour 
{   
    [SerializeField]
    PlayerControls playerControls;

    [SerializeField]
    Clues clues;

    List<GameObject> playersInfront = new List<GameObject>();

    void Awake()
    {
        playerControls.ButtonPressed += this.OnButtonPressed;
    }

    void OnButtonPressed(InputButton button)
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "Character")
        {
            return;
        }

        playersInfront.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "Character")
        {
            return;
        }

        playersInfront.Remove(other.gameObject);
    }
}