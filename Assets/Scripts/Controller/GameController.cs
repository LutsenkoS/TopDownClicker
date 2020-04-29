using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    public GameSettings settings;

    EnemySpawnController spawner;
    IMenuView menuView;
    IGameOverView gameOverView;

    private int missedEnemies;

    [Inject]
    public void Construct(IMenuView _menuView, IGameOverView _gameOverView, EnemySpawnController _spawner)
    {
        spawner = _spawner;
        menuView = _menuView;
        gameOverView = _gameOverView;
    }
    public void Start()
    {
        Init();
        ShowMenu(true);
    }
    void Init()
    {
        spawner.EnemyMiss += OnMissEnemy;
        spawner.BlackEnemyHit += OnHitBlackEnemy;
        menuView.OnStartBtnClick += OnStart;
        gameOverView.OnRestartClick += OnRestart;
        spawner.SetSpawnSettings(settings);
    }
    
    public void OnRestart()
    {
        ShowGameOver(false);
        spawner.StartSpawnEnemies();
        missedEnemies = 0;
    }

    public void OnStart()
    {
        ShowMenu(false);
        spawner.StartSpawnEnemies();
        missedEnemies = 0;
    }

    public void GameOver()
    {
        spawner.StopSpawnEnemies();
        ShowGameOver(true);
    }

    public void OnMissEnemy()
    {
        missedEnemies++;
        if(missedEnemies > settings.LoseLimit)
        {
            GameOver();
        }
    }

    public void OnHitBlackEnemy()
    {
        GameOver();
    }
    void ShowMenu(bool show)
    {
        menuView.Show(show);
    }

    void ShowGameOver(bool show)
    {
        gameOverView.Show(show);
    }
}
public enum GameState
{
    Menu,
    Game,
    GameOver
}