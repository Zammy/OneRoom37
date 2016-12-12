using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour 
{
    protected List<GameObject> interactableObjects = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.IsObjectInteractable(other))
        {
            return;
        }
        AddInteractable(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!this.IsObjectInteractable(other))
        {
            return;
        }
        RemoveInteractable(other.gameObject);
    }

    protected abstract bool IsObjectInteractable(Collider2D other);

    protected virtual void AddInteractable(GameObject other)
    {
        interactableObjects.Add(other.gameObject);

    }
    protected virtual void RemoveInteractable(GameObject other)
    {
        interactableObjects.Remove(other.gameObject);
    }

}
