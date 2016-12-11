using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour 
{
    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    Sprite[] characterSprites;

    public void SetCharacter(CharacterType character)
    {
        sprite.sprite = characterSprites[(int) character];
    }
}
