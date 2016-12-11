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

    Queue<bool[]> clueList = new Queue<bool[]>();

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

        foreach(var playerClue in playerClues)
        {
            bool[] clues = clueList.Dequeue();
            playerClue.SetFromBoolArray(clues);
        }

        foreach(var npcClue in npcClues)
        {
            bool[] clues = clueList.Dequeue();
            npcClue.SetFromBoolArray(clues);
        }
	}

    void CreateClueAllocationList() 
    {
        clueList.Enqueue(new bool[] { true, true, true });

        List<bool[]> verySuspect = new List<bool[]>();
        verySuspect.Add(new bool[] { true, true, false});
        verySuspect.Add(new bool[] { true, false, true});
        verySuspect.Add(new bool[] { false, true, true});
        verySuspect.xShuffle();
        clueList.Enqueue(verySuspect[0]);
        clueList.Enqueue(verySuspect[1]);
        clueList.Enqueue(verySuspect[2]);

        List<bool[]> lessSuspect = new List<bool[]>();
        lessSuspect.Add(new bool[] { true, false, false });
        lessSuspect.Add(new bool[] { false, true, false });
        lessSuspect.Add(new bool[] { false, false, true });
        clueList.Enqueue(lessSuspect[0]);
        clueList.Enqueue(lessSuspect[1]);
        clueList.Enqueue(lessSuspect[2]);
    }
}
