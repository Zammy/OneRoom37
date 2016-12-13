using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneSetup : MonoBehaviour 
{
    [SerializeField]
    private float countdownLength;
    [SerializeField]
    private float countdownTimeRemaining;

    [SerializeField]
    private PlayerPick[] playerStatusIndicators;

    [SerializeField]
    private Text counterText;

    [SerializeField]
    private string waitingForOnePlayer;
    [SerializeField]
    private string sceneStartsIn;

    private List<PlayerInfo> playerInfos = new List<PlayerInfo>();
    private List<CharacterType> charTypes;

	void Awake () 
    {
        counterText.text = waitingForOnePlayer;

        InitCharTypes();
    }

    readonly KeyCode[] keyCodes = new KeyCode[]
    {
        KeyCode.Joystick1Button0,
        KeyCode.Joystick2Button0,
        KeyCode.Joystick3Button0,
        KeyCode.Joystick4Button0,
    };

    readonly KeyCode[] cheatKeyCodes = new KeyCode[]
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
    };

	void Update () 
    {
        for (int i = 0; i < keyCodes.Length; i++) 
        {
            if (Input.GetKeyDown(keyCodes[i]) ||
                Input.GetKeyDown(cheatKeyCodes[i]))
            {
                ActiveButtonPressed(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ActiveButtonPressed();
        }

        if (UpdateCountdown()) 
        {
            StartGame();
        }
    }

    void ActiveButtonPressed(int controllerIndex = -1)
    {
        var existingPlayerInfo = this.playerInfos.Find(p => p.ControllerIndex == controllerIndex);
        if (existingPlayerInfo != null)
        {
            PlayerUnpickedAt(existingPlayerInfo.PlayerIndex);

            if (existingPlayerInfo.ChracterType == CharacterType.Detective)
            {
                charTypes.Insert(0, CharacterType.Detective);
            }
            else
            {
                charTypes.Add(existingPlayerInfo.ChracterType);
            }

            this.playerInfos.Remove( existingPlayerInfo );

            if (this.playerInfos.Count == 0)
            {
                counterText.text = waitingForOnePlayer;
                countdownTimeRemaining = -1f;
            }
        }
        else
        {
            int playerIndex = FreePlayerIndex();
            CharacterType charType = charTypes[0];
            charTypes.RemoveAt(0);

            PlayerPickedCharacterAt(charType, playerIndex);

            playerInfos.Add(new PlayerInfo()
            {
                ChracterType = charType,
                PlayerIndex = playerIndex,
                ControllerIndex = controllerIndex,
                ControllerType = controllerIndex == -1 ? ControllerType.Keyboard : ControllerType.Gamepad,
            });

            countdownTimeRemaining = countdownLength;
        }
    }

    void InitCharTypes()
    {
        charTypes = new List<CharacterType>( (CharacterType[]) Enum.GetValues(typeof(CharacterType)) );
    }

    int FreePlayerIndex()
    {
        var indexes = new List<int>{ 0, 1, 2, 3};
        playerInfos.ForEach(p =>
        {
            indexes.Remove(p.PlayerIndex);
        });

        //unsafe, exception better option if no indexes
        return indexes[0];
    }

    //returns if countdown has finished
    bool UpdateCountdown()
    {
        //if already has passed countdown
        if (countdownTimeRemaining < 0)
        {
            return false;
        }

        countdownTimeRemaining -= Time.deltaTime;

        if (countdownTimeRemaining < 0)
        {
            counterText.text = "";
            return true;
        }
        counterText.text = sceneStartsIn + Mathf.Ceil(countdownTimeRemaining) + "...";
        return false;
    }

    void PlayerPickedCharacterAt(CharacterType charType, int playerIndex)
    {
        var playerPick = playerStatusIndicators[playerIndex];
        playerPick.gameObject.SetActive(true);
        playerPick.SetCharacter(charType);
    }

    void PlayerUnpickedAt(int playerIndex)
    {
        playerStatusIndicators[playerIndex].gameObject.SetActive(false);
    }

    void StartGame()
    {
        PlayerInfo.PlayerInfos = playerInfos.ToArray();
        SceneManager.LoadScene("MurderScene");
    }

}
