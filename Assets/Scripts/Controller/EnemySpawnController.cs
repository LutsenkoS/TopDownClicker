using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawnController: ITickable
{
    public delegate void EnemyEventsDelegate();

    public event EnemyEventsDelegate EnemyMiss;
    public event EnemyEventsDelegate BlackEnemyHit;

    EnemyBlackController.Factory enemyBlackFactory;
    EnemyBlueController.Factory enemyBlueFactory;
    EnemyRedController.Factory enemyRedFactory;
    List<IEnemy> spawnedEnemies;
    bool spawnEnemies;
    float spawnTimer;
    private Vector2 SpawnRange;
    private float spawnInterval;

    public EnemySpawnController(EnemyBlackController.Factory enemyBlack, EnemyBlueController.Factory enemyBlue, EnemyRedController.Factory enemyRed)
    {
        enemyBlackFactory = enemyBlack;
        enemyBlueFactory = enemyBlue;
        enemyRedFactory = enemyRed;

    }
    public void SetSpawnSettings(GameSettings settings)
    {
        SpawnRange = settings.EnemySpawnRange;
        spawnInterval = settings.EnemySpawnInterval;
    }
    public void StartSpawnEnemies()
    {
        if(spawnedEnemies == null)
            spawnedEnemies = new List<IEnemy>();
        spawnEnemies = true;
    }

    public void StopSpawnEnemies()
    {
        spawnEnemies = false;
        DestroyAllEnemies();
    }

    public void Tick()
    {
        SpawnEnemies();
    }
    public void DestroyAllEnemies()
    {
        foreach (IEnemy enemy in spawnedEnemies)
        {
            enemy.EnemyState = EnemyStates.Dead;
        }
        spawnedEnemies.Clear();
    }
    public void OnEnemyBeyondScreen(IEnemy enemy)
    {
        if (enemy.EnemyType != EnemyType.Black)
        {
            EnemyMiss?.Invoke();
        }
        RemoveEnemy(enemy);
    }

    private void SpawnEnemies()
    {
        if (!spawnEnemies)
            return;

        if (NextSpawn())
        {
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        int enemyNumber = Random.Range(0, 3);
        Vector3 startPos = new Vector3(Random.Range(-SpawnRange.x, SpawnRange.x), SpawnRange.y, 0);

        IEnemy enemy;
        switch (enemyNumber)
        {
            case 0: {
                    enemy = enemyBlackFactory.Create();
                    break;
                }
            case 1:
                {
                    enemy = enemyBlueFactory.Create();
                    break;
                }
            case 2:
                {
                    enemy = enemyRedFactory.Create();
                    break;
                }
            default:
                enemy = enemyRedFactory.Create();
                break;
        }
        enemy.SetStartPosition(startPos);
        spawnedEnemies.Add(enemy);
        enemy.EnemyHit += OnEnemyHit;
    }
   
    private void OnEnemyHit(IEnemy enemy)
    {  
        if(enemy.EnemyType == EnemyType.Black)
        {
            BlackEnemyHit?.Invoke();   
        }
        else
        {
            RemoveEnemy(enemy);
        }
    }
    private void RemoveEnemy(IEnemy enemy)
    {
        spawnedEnemies.Remove(enemy);
        enemy.EnemyState = EnemyStates.Dead;
    }
   
    bool NextSpawn()
    {
        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0;
            return true;
        }
        spawnTimer += Time.deltaTime;
        return false;
    }
}
public enum EnemyStates
{
    Running,
    Dead
}

public enum EnemyType
{
    Red,
    Blue,
    Black
}