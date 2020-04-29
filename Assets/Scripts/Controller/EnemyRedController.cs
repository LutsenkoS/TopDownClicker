using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.EventSystems;
using System;

public class EnemyRedController : MonoBehaviour, IEnemy
{
    public EnemyData enemyRed;
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
    public EnemyType EnemyType { get; set; } = EnemyType.Red;
    EnemyMoveController enemyMove;

    EnemyStates _enemyState;

    void Start()
    {
        enemyMove = new EnemyMoveController(transform, enemyRed.Speed, false);
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

    void OnEnemyStateChanged()
    {
        if (EnemyState == EnemyStates.Dead)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnemyHit()
    {
        EnemyHit?.Invoke(this);
    }
    public class Factory : PlaceholderFactory<EnemyRedController> { }

}
