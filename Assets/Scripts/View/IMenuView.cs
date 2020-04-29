using System;

public interface IMenuView
{
    event ButtonCallback OnStartBtnClick;
    void Show(bool show);
}