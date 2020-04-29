using UnityEngine;
using UnityEditor;
using Zenject;
using System;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using System.Collections;

public class EnemyBlackController : MonoBehaviour, IEnemy
{
    public EnemyData enemyBlack;
    public event Action<IEnemy> EnemyHit;

    public EnemyStates EnemyState 
    { 
        get { return _enemyState; }
        set {
            _enemyState = value;
            OnEnemyStateChanged();
        } 
    }
    public EnemyType EnemyType { get; set; } = EnemyType.Black;
    EnemyMoveController enemyMove;

    EnemyStates _enemyState;

    void Start()
    {
        enemyMove = new EnemyMoveController(transform, enemyBlack.Speed, true, enemyBlack.SideMoveInterval, enemyBlack.MoveInterval);
    }
    void Update()
    {
        Move();
    }

    public void Move()
    {
        enemyMove.Move();
    }
    public void SetStartPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void OnEnemyHit()
    {
        EnemyHit?.Invoke(this);
    }

    void OnEnemyStateChanged()
    {
        if(EnemyState == EnemyStates.Dead)
        {
            Destroy(gameObject);
        }
    }

    public class Factory : PlaceholderFactory<EnemyBlackController> { }
}