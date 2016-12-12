using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum WinState
{
    DetectiveFoundMurderer,
    DetectiveDidNotFoundMurderer,
    MurdererEscaped
}

public class GameEnd : MonoBehaviour 
{
    [SerializeField]
    Text infoText;

    [SerializeField]
    string[] gameEndTexts;

    public int PlayerIndex
    {
        get;
        set;
    }

    public WinState WinState
    {
        get
        {
            return _winState;
        }
        set
        {
            _winState = value;
            infoText.text = string.Format(gameEndTexts[(int) _winState], PlayerIndex);
        }
    }

    void Start()
    {
        DOTween.Sequence()
            .AppendInterval(5f)
            .AppendCallback(() =>
            {
                SceneManager.LoadScene("SceneSetup");
            });
    }

    WinState _winState;
}
