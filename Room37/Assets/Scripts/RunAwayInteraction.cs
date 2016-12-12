using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunAwayInteraction : BaseInteraction
{
    protected override bool IsObjectInteractable(Collider2D other)
    {
        return other.tag == "Player" || other.tag == "Character";
    }

    protected override void AddInteractable(GameObject other)
    {
        base.AddInteractable(other);

        if (other.tag == "Player")
        {
            var playerControls = other.GetComponent<PlayerControls>();
            var clue = other.GetComponentInChildren<Clues>();
            var playerInteraction = other.GetComponentInChildren<BasePlayerInteraction>();
            if (!playerControls)
            {
                throw new UnityException(String.Format("Gameobject with Player tag has no PlayerControls"));
            }

            playerInteraction.IsInteracting = true;

            ConfirmationDialog dialog = UIManager.Instance.CreateDialogOnPos(other.transform.position);
            dialog.Type = DialogType.RunAway;
            dialog.PlayerControls = playerControls;
            dialog.Show(yes =>
            {
                playerInteraction.IsInteracting = false;

                if (!yes)
                {
                    return;
                }

                if (clue.IsMurderer())
                {
                    UIManager.Instance.ShowGameEnded(playerControls.PlayerNumber, WinState.MurdererEscaped);
                }
                else
                {
                    UIManager.Instance.ShowInfoMessage(string.Format("Player {0} eliminated because not the murderer!", playerControls.PlayerNumber));
                    Destroy(playerControls.gameObject);
                }
            });
        }
    }
}
