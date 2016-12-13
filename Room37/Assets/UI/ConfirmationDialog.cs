using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public enum DialogType
{
    RunAway,
    IsMurderer
}

public class ConfirmationDialog : MonoBehaviour 
{
    [SerializeField]
    Text title;

    [SerializeField]
    GameObject yesSelect;

    [SerializeField]
    GameObject noSelect;

    public DialogType Type
    {
        get
        {
            return _dialogType;
        }
        set
        {
            _dialogType = value;
            switch (value)
            {
                case DialogType.IsMurderer:
                {
                    title.text = "Is Murderer?";
                    break;
                }
                case DialogType.RunAway:
                {
                    title.text = "Flee the scene?";
                    break;
                }
                default:
                    break;
            }

        }
    }

    public PlayerControls PlayerControls
    {
        get
        {
            return _playerControls;
        }
        set 
        {
            _playerControls = value;
            _playerControls.InputCommandStream += this.OnCommandReceived;
            _playerControls.ButtonPressed += this.OnButtonPressed;
        }
    }

    public void Show( Action<bool> callback)
    {
        _callback = callback;
        transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
    }

    void Awake()
    {
        transform.localScale = Vector3.zero;
    }


    void OnDestroy()
    {
        if (PlayerControls)
        {
            PlayerControls.InputCommandStream -= this.OnCommandReceived;
            PlayerControls.ButtonPressed -= this.OnButtonPressed;

        }
    }

    private bool IsYesSelected 
    {
        get
        {
            return yesSelect.activeSelf;
        }
        set
        {
            yesSelect.SetActive(value);
            noSelect.SetActive(!value);
        }
    }

    void OnCommandReceived(InputCommand inputCommand)
    {
        this.IsYesSelected = inputCommand == InputCommand.InterfaceInteractRight;
        this.IsYesSelected = !(inputCommand == InputCommand.InterfaceInteractLeft);
    }

    void OnButtonPressed(InputButton inputButton)
    {
        if (_callback != null)
        {
            _callback(IsYesSelected);
        }

        transform.DOScale(Vector3.zero, 0.4f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                Destroy (this);
            });
    }

    PlayerControls _playerControls;
    DialogType _dialogType;
    Action<bool> _callback;
}
