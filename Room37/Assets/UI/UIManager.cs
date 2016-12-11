using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    public static UIManager Instance;

    [SerializeField]
    GameObject infoFeedbackWidgetPrefab;

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

        Vector2 viewPortPoint = canvas.WorldToCanvas(worldSpaceWidgetPos);

        var infoFeedbackGo = (GameObject) Instantiate(infoFeedbackWidgetPrefab);
        infoFeedbackGo.transform.SetParent(this.transform);
        infoFeedbackGo.transform.localScale = Vector3.one;
        var infoFeedbackWidget = infoFeedbackGo.GetComponent<InfoFeedbackWidget>();

        var infoRectTrans = infoFeedbackWidget.transform as RectTransform;
        infoRectTrans.anchoredPosition = viewPortPoint - (infoRectTrans.sizeDelta / 2f);
 
        infoFeedbackWidget.ShowMatches(matches);
    }
}
