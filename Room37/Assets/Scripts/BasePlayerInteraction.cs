using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePlayerInteraction : BaseInteraction 
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


    void Awake()
    {
        playerControls.ButtonPressed += this.OnButtonPressed;
    }

    protected override bool IsObjectInteractable(Collider2D other)
    {
        return other.tag == "Player" || other.tag == "Character";
    }

    protected virtual void OnButtonPressed(InputButton button) 
    {
        if (button != InputButton.Interact)
        {
            return;
        }

        if (this.interactableObjects.Count == 0)
        {
            return;
        }

        if (this.IsInteracting)
        {
            return;
        }

        var other = interactableObjects[0];
        var otherBaseInteraction = other.GetComponentInChildren<BasePlayerInteraction>();
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

        AudioManager.Instance.PlayTalkSound();

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
