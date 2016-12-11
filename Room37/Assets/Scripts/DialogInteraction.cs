using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : MonoBehaviour 
{   
    [SerializeField]
    PlayerControls playerControls;

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

        UIManager.Instance.ShowInfoFeedback(this.transform.position, playersInfront[0].transform.position, 3);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        playersInfront.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        playersInfront.Remove(other.gameObject);
    }
}