using System;

public interface IGameOverView
{
    event ButtonCallback OnRestartClick;
    void Show(bool show);
}