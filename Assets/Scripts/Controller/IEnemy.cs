using System;
using UnityEngine;

public interface IEnemy
{
    event Action<IEnemy> EnemyHit;
    EnemyType EnemyType { get; set; }
    EnemyStates EnemyState { get; set; }
    void SetStartPosition(Vector3 position);
    void Move();
    void OnEnemyHit();
}
