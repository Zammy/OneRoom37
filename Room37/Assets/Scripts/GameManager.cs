using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    Transform[] startingPositions;

    [SerializeField]
    GameObject playerPrefab;

    List<bool[]> clueList = new List<bool[]>();
    List<bool[]> remainingCluesList = new List<bool[]>();

    void Start () 
    {
        CreateClueAllocationList();

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

            if (playerInfo.ChracterType != CharacterType.Detective) {
                var clueSetup = playerGo.GetComponentInChildren<Clues>();
                int clueListIndex = Random.Range(0, clueList.Count);
                clueSetup.Motive = clueList[clueListIndex][0];
                clueSetup.Means = clueList[clueListIndex][1];
                clueSetup.Opportunity = clueList[clueListIndex][2];
                clueList.RemoveAt(clueListIndex);
            }
            
            
        }

        remainingCluesList.Add(clueList[0]);

	}

    void CreateClueAllocationList() {
        clueList.Add(new bool[] { true, true, true });

        List<bool[]> verySuspect = new List<bool[]>();
        verySuspect.Add(new bool[] { true, true, false});
        verySuspect.Add(new bool[] { true, false, true});
        verySuspect.Add(new bool[] { false, true, true});
        verySuspect.xShuffle();
        clueList.Add(verySuspect[0]);
        clueList.Add(verySuspect[1]);
        clueList.Add(verySuspect[2]);

        List<bool[]> lessSuspect = new List<bool[]>();
        lessSuspect.Add(new bool[] { true, false, false });
        lessSuspect.Add(new bool[] { false, true, false });
        lessSuspect.Add(new bool[] { false, false, true });
        remainingCluesList.Add(lessSuspect[0]);
        remainingCluesList.Add(lessSuspect[1]);
        remainingCluesList.Add(lessSuspect[2]);
    }

}
