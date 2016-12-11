using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoFeedbackWidget : MonoBehaviour 
{
    [SerializeField]
    Image[] ticks;

    [SerializeField]
    Image background;

	public void ShowMatches(int numMatches)
    {
        background.enabled = true;

        var sequence = DOTween.Sequence();

        for (int i = 0; i < numMatches; i++)
        {
            var tick = ticks[i];
            tick.gameObject.SetActive(true);
            tick.transform.localScale = Vector3.zero;

            sequence.Append(tick.transform.DOScale(Vector3.one, .65f).SetEase(Ease.OutBack));
            sequence.AppendInterval(.25f);
        }

        sequence.AppendInterval(.5f);

        var disappear = DOTween.Sequence();
        for (int i = 0; i < numMatches; i++)
        {
            var tick = ticks[i];
            disappear.Insert(0, tick.transform.DOScale(Vector3.zero, .4f));
        }

        sequence.Append(disappear);

        sequence.AppendCallback(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
