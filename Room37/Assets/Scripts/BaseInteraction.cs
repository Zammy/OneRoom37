using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteraction : MonoBehaviour 
{
    [SerializeField]
    PlayerControls playerControls;

    protected List<GameObject> playersInfront = new List<GameObject>();

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

    void Awake()
    {
        playerControls.ButtonPressed += this.OnButtonPressed;
    }

    protected virtual void OnButtonPressed(InputButton button) {}
}
