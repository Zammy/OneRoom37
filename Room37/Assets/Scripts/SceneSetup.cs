using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneSetup : MonoBehaviour {

    [SerializeField]
    private float countdownLength;
    [SerializeField]
    private float countdownTimeRemaining;

    public bool countdownActive { get; private set; }

    public bool[] activePlayers = new bool[4] { false, false, false, false };

    [SerializeField]
    private Text[] playerStatusIndicators;

    [SerializeField]
    private Text counterText;

    [SerializeField]
    private string waitingForOnePlayer;
    [SerializeField]
    private string sceneStartsIn;

    private List<PlayerInfo> playerInfo = new List<PlayerInfo>();
    private List<CharacterType> charTypes = new List<CharacterType>( (CharacterType[]) Enum.GetValues(typeof(CharacterType)) );

    void TogglePlayer(int playerNumber) {
        activePlayers[playerNumber] = !(activePlayers[playerNumber]);
        //Debug.Log("Player " + (playerNumber + 1) + " active: " + activePlayers[playerNumber].ToString());
        if (activePlayers[playerNumber] == true) {
            playerStatusIndicators[playerNumber].enabled = true;
            AddPlayerInfo(playerNumber);
            if (countdownActive == false) {
                countdownTimeRemaining = countdownLength;
                countdownActive = true;
            }
        } else {
            playerStatusIndicators[playerNumber].enabled = false;
            RemovePlayerInfo(playerNumber);
            countdownActive = false;
            counterText.text = waitingForOnePlayer;
            foreach (bool remainingPlayer in activePlayers) {
                if (remainingPlayer == false) {
                    continue;
                }
                countdownActive = true;
            }
        }
    }

	void Awake () {
        countdownActive = false;
        counterText.text = waitingForOnePlayer;
    }

    readonly KeyCode[] keyCodes = new KeyCode[]
    {
        KeyCode.Joystick1Button0,
        KeyCode.Joystick2Button0,
        KeyCode.Joystick3Button0,
        KeyCode.Joystick4Button0,
    };

	void Update () {
        for (int i = 0; i < keyCodes.Length; i++) {
            if (Input.GetKeyDown(keyCodes[i])) {
                TogglePlayer(i);
            }
        }

        if (countdownActive) {
            if (countdownTimeRemaining > 0) {
                countdownTimeRemaining -= Time.deltaTime;
                counterText.text = sceneStartsIn + Mathf.Ceil(countdownTimeRemaining) + "...";
            } else {
                playerInfo.xShuffle();
                playerInfo[0].ChracterType = CharacterType.Detective;
                charTypes.Remove(CharacterType.Detective);
                charTypes.xShuffle();
                for (int i = 1; i < playerInfo.Count; i++ ) {
                    playerInfo[i].ChracterType = charTypes[0];
                    charTypes.RemoveAt(0);
                }
                PlayerInfo.PlayerInfos = playerInfo.ToArray();
                SceneManager.LoadScene("Test");
            }
        }
    }

    void AddPlayerInfo(int playerIndex) {
        PlayerInfo newPlayer = new PlayerInfo();
        newPlayer.PlayerIndex = playerIndex;
        playerInfo.Add(newPlayer);
    }

    void RemovePlayerInfo(int playerIndex) {
        for (int i = 0; i < playerInfo.Count; i++ ) {
            if (playerInfo[i].PlayerIndex == playerIndex) {
                playerInfo.RemoveAt(i);
                break;
            }
        }
    }
}
