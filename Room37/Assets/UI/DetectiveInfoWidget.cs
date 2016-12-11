using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DetectiveInfoWidget : MonoBehaviour 
{
    [SerializeField]
    Sprite[] clueStateSprites;

    [SerializeField]
    Image[] cluesImages;

    public void ShowMatches(Dossier dossier)
    {
        var sequence = DOTween.Sequence();

        for (int i = 0; i < dossier.ClueStates.Length; i++)
        {
            var clueImage = cluesImages[i];
            clueImage.sprite = clueStateSprites[(int)dossier.ClueStates[i]];
            
            clueImage.transform.localScale = Vector3.zero;

            sequence.Append(clueImage.transform.DOScale(Vector3.one, .65f).SetEase(Ease.OutBack));
            sequence.AppendInterval(.25f);
        }

        sequence.AppendInterval(.5f);

        var disappear = DOTween.Sequence();
        for (int i = 0; i < dossier.ClueStates.Length; i++)
        {
            var clueImage = cluesImages[i];
            disappear.Insert(0, clueImage.transform.DOScale(Vector3.zero, .4f));
        }

        sequence.Append(disappear);

        sequence.AppendCallback(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
