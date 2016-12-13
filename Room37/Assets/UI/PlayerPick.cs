using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPick : MonoBehaviour 
{
    [SerializeField]
    Image image;

    [SerializeField]
    Text text;

    [SerializeField]
    Sprite[] characterSprites;

	public void SetCharacter(CharacterType type)
    {
        text.text = type.ToString();
        image.sprite = characterSprites[(int)type];
    }

}
