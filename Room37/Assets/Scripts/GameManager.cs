using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    Transform[] startingPositions;

    [SerializeField]
    Transform[] npcPositions;

    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    GameObject npcPrefab;

    [SerializeField]
    GameObject detective;

    List<bool[]> clueList = new List<bool[]>();

    void Start () 
    {
        CreateClueAllocationList();

        var playerClues = new List<Clues>();

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
                var clues = playerGo.GetComponentInChildren<Clues>();
                playerClues.Add(clues);
            }

            var playerControls = playerGo.GetComponent<PlayerControls>();
            playerControls.PlayerNumber = playerInfo.PlayerIndex;
            playerControls.ControllerIndex = playerInfo.ControllerIndex;
            playerControls.ControllerType = playerInfo.ControllerType;
        }

        var npcClues = new List<Clues>();
        var npcPoses = new Stack<Transform>(npcPositions);

        for (int i = PlayerInfo.PlayerInfos.Length; i < 7; i++)
        {
            var npcGo = (GameObject) Instantiate(npcPrefab);
            npcGo.transform.localScale = Vector3.one;
            npcGo.transform.position = npcPoses.Pop().position;
            var clues = npcGo.GetComponent<Clues>();
            npcClues.Add(clues);
        }

        playerClues.xShuffle();
        npcClues.xShuffle();

        if (playerClues.Count == 1)
        {
            clueList.xShuffle();
        }

        foreach(var playerClue in playerClues)
        {
            bool[] clues = clueList.xPop();
            playerClue.SetFromBoolArray(clues);
        }

        foreach(var npcClue in npcClues)
        {
            bool[] clues = clueList.xPop();
            npcClue.SetFromBoolArray(clues);
        }
	}

    void CreateClueAllocationList() 
    {
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
        clueList.Add(lessSuspect[0]);
        clueList.Add(lessSuspect[1]);
        clueList.Add(lessSuspect[2]);
    }
}
