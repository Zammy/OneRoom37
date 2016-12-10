using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public static class StoryData
{
    public static Dictionary<char, char> GetStoryForIndex(int index)
    {
        switch (index)
        {
            case 0:
            {
                return new Dictionary<char, char>
                {
                    {'A', 'B'},
                    {'B', 'G'},
                    {'C', 'A'},
                    {'D', 'A'},
                    {'E', 'F'},
                    {'G', 'A'},
                    {'R', 'C'},
                    {'F', 'A'}
                };
            }
            case 1:
            {
                return new Dictionary<char, char>
                {
                    {'A', 'G'},
                    {'B', 'E'},
                    {'C', 'A'},
                    {'D', 'R'},
                    {'E', 'B'},
                    {'G', 'D'},
                    {'R', 'A'},
                    {'F', 'A'}
                };
            }
            case 2:
            {
                return new Dictionary<char, char>
                {
                    {'A', 'B'},
                    {'B', 'G'},
                    {'C', 'E'},
                    {'D', 'B'},
                    {'E', 'R'},
                    {'G', 'A'},
                    {'R', 'C'},
                    {'F', 'A'}
                };
            }
            default:
                return null;
        }
    }
}

public enum Characters
{
    Mike = 0,
    Brown = 1,
    Kristoff = 2
}


public class WitnessInteraction : MonoBehaviour, IPointerDownHandler
{
    public Characters Charcter;
    public Dictionary<char, char> Story;

    [SerializeField]
    Inventory Inventory;

    [SerializeField]
    GameObject InteractingSprite;

    [SerializeField]
    GameObject NonInteractingSprite;

    public bool IsInteracting
    {
        get
        {
            return _isInteracting;
        }
        set
        {
            _isInteracting = value;

            InteractingSprite.gameObject.SetActive(_isInteracting);
            NonInteractingSprite.gameObject.SetActive(!_isInteracting);
        }
    }

    void Awake()
    {
        this.Story = StoryData.GetStoryForIndex((int)this.Charcter);
    }   

    #region IPointerDownHandler implementation
    public void OnPointerDown(PointerEventData eventData)
    {
        Inventory.DisplayWithWitness(this);
    }
    #endregion

    bool _isInteracting = false;
}
