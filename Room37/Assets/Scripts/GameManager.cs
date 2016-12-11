using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    Transform[] startingPositions;

    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    GameObject detective;

	void Start () 
    {
        for (int i = 0; i < PlayerInfo.PlayerInfos.Length; i++)
        {
            var playerInfo = PlayerInfo.PlayerInfos[i];
            GameObject playerGo;
            if (playerInfo.ChracterType == CharacterType.Detective)
            {
                playerGo = detective;
            }
            else
            {
                playerGo = (GameObject) Instantiate(playerPrefab);
                playerGo.transform.localScale = Vector3.one;
            }
            playerGo.transform.position = startingPositions[i].transform.position;

            if (playerInfo.ChracterType != CharacterType.Detective)
            {
                var visual = playerGo.GetComponentInChildren<PlayerVisual>();
                visual.SetCharacter(playerInfo.ChracterType);
            }

            var playerControls = playerGo.GetComponent<PlayerControls>();
            playerControls.PlayerNumber = playerInfo.PlayerIndex;
        }
	}   
}
