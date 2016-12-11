using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    Transform[] startingPositions;

    [SerializeField]
    GameObject playerPrefab;

	void Start () 
    {
        for (int i = 0; i < PlayerInfo.PlayerInfos.Length; i++)
        {
            var playerInfo = PlayerInfo.PlayerInfos[i];
            var playerGo = (GameObject) Instantiate(playerPrefab);
            playerGo.transform.localScale = Vector3.one;
            playerGo.transform.position = startingPositions[i].transform.position;

            var visual = playerGo.GetComponentInChildren<PlayerVisual>();
            visual.SetCharacter(playerInfo.ChracterType);

            var playerControls = playerGo.GetComponent<PlayerControls>();
            playerControls.PlayerNumber = playerInfo.PlayerIndex;
        }

	}

}
