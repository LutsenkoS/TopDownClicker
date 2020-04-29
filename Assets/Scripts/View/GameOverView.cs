using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverView : MonoBehaviour, IGameOverView
{
    public GameObject panel;
    public Button restartButton;
    public event ButtonCallback OnRestartClick;
    
    private void Start()
    {
        restartButton.onClick.AddListener(OnRestartBtnClick);
    }
    public void Show(bool show)
    {
        panel.SetActive(show);
    }
    private void OnRestartBtnClick()
    {
        OnRestartClick?.Invoke();
    }
}
