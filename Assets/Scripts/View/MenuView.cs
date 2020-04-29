using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public delegate void ButtonCallback();

public class MenuView : MonoBehaviour, IMenuView
{
    public GameObject panel;
    public Button startButton;
    public event ButtonCallback OnStartBtnClick;
    

    void Start()
    {
        startButton.onClick.AddListener(OnStartBtnClicked);
    }
    public void Show(bool show)
    {
        panel.SetActive(show);
    }
    private void OnStartBtnClicked()
    {
        OnStartBtnClick?.Invoke();
    }
}
