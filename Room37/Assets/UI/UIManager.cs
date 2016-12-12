using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    public static UIManager Instance;

    [SerializeField]
    GameObject infoFeedbackWidgetPrefab;

    [SerializeField]
    GameObject detectiveInfoWidgetPrefab;

    [SerializeField]
    GameObject confirmDialogPrefab;

    [SerializeField]
    GameObject gameEndPrefab;


    Canvas canvas;

    void Awake()
    {
        Instance = this;
        canvas = GetComponent<Canvas>();
    }

	public void ShowInfoFeedback(Vector3 character1Pos, Vector3 character2Pos, int matches)
    {
        Vector3 worldSpaceWidgetPos;
        worldSpaceWidgetPos.x = (character1Pos.x + character2Pos.x)/2 + 35;
        worldSpaceWidgetPos.y = Mathf.Max(character1Pos.y, character2Pos.y) + 75;
        worldSpaceWidgetPos.z = 0;

        Vector2 viewPortPoint = canvas.xWorldToCanvas(worldSpaceWidgetPos);

        var infoFeedbackGo = (GameObject) Instantiate(infoFeedbackWidgetPrefab);
        infoFeedbackGo.transform.SetParent(this.transform);
        infoFeedbackGo.transform.localScale = Vector3.one;
        var infoFeedbackWidget = infoFeedbackGo.GetComponent<InfoFeedbackWidget>();

        var infoRectTrans = infoFeedbackWidget.transform as RectTransform;
        infoRectTrans.anchoredPosition = viewPortPoint - (infoRectTrans.sizeDelta / 2f);
 
        infoFeedbackWidget.ShowMatches(matches);
    }


    public void ShowDetectiveInfo(Vector3 character1Pos, Dossier dossier)
    {
        Vector3 worldSpaceWidgetPos;
        worldSpaceWidgetPos.x = character1Pos.x + 50;
        worldSpaceWidgetPos.y = character1Pos.y + 55;
        worldSpaceWidgetPos.z = 0;

        Vector2 viewPortPoint = canvas.xWorldToCanvas(worldSpaceWidgetPos);

        var detectiveInfoWidgetGo = (GameObject) Instantiate(detectiveInfoWidgetPrefab);
        detectiveInfoWidgetGo.transform.SetParent(this.transform);
        detectiveInfoWidgetGo.transform.localScale = Vector3.one;
        var detectiveInfoWidget = detectiveInfoWidgetGo.GetComponent<DetectiveInfoWidget>();

        var infoRectTrans = detectiveInfoWidget.transform as RectTransform;
        infoRectTrans.anchoredPosition = viewPortPoint - (infoRectTrans.sizeDelta / 2f);
 
        detectiveInfoWidget.ShowMatches(dossier);
    }

    public ConfirmationDialog CreateDialogOnPos(Vector3 pos)
    {
        Vector2 viewPortPoint = canvas.xWorldToCanvas(pos);

        var confirmDialogGo = (GameObject) Instantiate(confirmDialogPrefab);
        confirmDialogGo.transform.SetParent(this.transform);
        confirmDialogGo.transform.localScale = Vector3.one;
        var confirmDialog = confirmDialogGo.GetComponent<ConfirmationDialog>();
        var confirmDialogRectTrans = (RectTransform)confirmDialogGo.transform ;
        confirmDialogRectTrans.anchoredPosition = viewPortPoint;// - (confirmDialogRectTrans.sizeDelta / 2f);
        return confirmDialog;
    }

    public void ShowInfoMessage(string text)
    {
        Debug.Log(text);
        //TODO: add some text at the at top of the game

    }

    public void ShowGameEnded(int wonPlayerIndex, WinState winState)
    {
        var gameEndGo = Instantiate(gameEndPrefab, transform.position, Quaternion.identity);
        gameEndGo.transform.SetParent(this.transform);
        gameEndGo.transform.localScale = Vector3.one;
        var gameEnd = gameEndGo.GetComponent<GameEnd>();
        gameEnd.PlayerIndex = wonPlayerIndex;
        gameEnd.WinState = winState;
    }
}
