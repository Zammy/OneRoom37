using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour 
{
    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    Sprite[] characterSprites;

    [SerializeField]
    Sprite[] characterSpritesInteracting;

    public bool IsInteracting
    {
        get
        {
            return _isInteracting;
        }    
        set
        {
            _isInteracting = value;
            SetCharacterSprite(_characterType, _isInteracting);
        }
    }

    public void SetCharacter(CharacterType character)
    {
        _characterType = character;
        this.IsInteracting = false;
    }

    void SetCharacterSprite(CharacterType character, bool interacting = false)
    {
        if (interacting)
        {
            sprite.sprite = characterSpritesInteracting[(int)character];
        }
        else
        {
            sprite.sprite = characterSprites[(int)character];
        }
    }

    bool _isInteracting = false;

    CharacterType _characterType = CharacterType.Detective;
}
