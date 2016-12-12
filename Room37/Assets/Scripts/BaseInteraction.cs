using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseInteraction : MonoBehaviour 
{
    [SerializeField]
    PlayerControls playerControls;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    PlayerVisual playerVisual;

    public bool IsInteracting
    {
        get
        {
            return _isInteracting;
        }    
        set
        {
            _isInteracting = value;
            playerVisual.IsInteracting = value;
            playerMovement.IsInteracting = value;
        }
    }

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

    protected virtual void OnButtonPressed(InputButton button) 
    {
        if (button != InputButton.Interact)
        {
            return;
        }

        if (playersInfront.Count == 0)
        {
            return;
        }

        if (this.IsInteracting)
        {
            return;
        }

        var other = playersInfront[0];
        var otherBaseInteraction = other.GetComponentInChildren<BaseInteraction>();
        if (otherBaseInteraction && otherBaseInteraction.IsInteracting)
        {
            return;
        }

        this.transform.xLookAt(other.transform.position);
        other.transform.xLookAt(this.transform.position);
        this.IsInteracting = true;
        if (otherBaseInteraction)
        {
            otherBaseInteraction.IsInteracting = true;
        }

        InteractWithNPC(other);

        DOTween.Sequence()
           .AppendInterval(2f)
           .AppendCallback(() => 
           {
                this.IsInteracting = false;
                if (otherBaseInteraction)
                {
                    otherBaseInteraction.IsInteracting = false;
                }           
            })
           .Play();
    }

    protected virtual void InteractWithNPC(GameObject interectWith)
    {
    }

    bool _isInteracting = false;
}
