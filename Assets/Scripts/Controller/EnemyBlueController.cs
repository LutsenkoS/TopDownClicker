using UnityEngine;
using System.Collections;
using Zenject;
using System;
using UnityEngine.EventSystems;

public class EnemyBlueController : MonoBehaviour, IEnemy
{
    public EnemyData enemyBlue;
    public event Action<IEnemy> EnemyHit;

    public EnemyStates EnemyState
    {
        get { return _enemyState; }
        set
        {
            _enemyState = value;
            OnEnemyStateChanged();
        }
    }
    public EnemyType EnemyType { get; set; } = EnemyType.Blue;
    EnemyMoveController enemyMove;

    EnemyStates _enemyState;

    void Start()
    {
        enemyMove = new EnemyMoveController(transform, enemyBlue.Speed, true, enemyBlue.SideMoveInterval, enemyBlue.MoveInterval);
    }
    public void SetStartPosition(Vector3 position)
    {
        transform.position = position;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        enemyMove.Move();
    }
    public void OnEnemyHit()
    {
        EnemyHit?.Invoke(this);
    }

    void OnEnemyStateChanged()
    {
        if (EnemyState == EnemyStates.Dead)
        {
            Destroy(gameObject);
        }
    }

    public class Factory : PlaceholderFactory<EnemyBlueController> { }
}
