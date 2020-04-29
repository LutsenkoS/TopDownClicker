using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesRemoveController : MonoBehaviour
{
    [Inject]
    EnemySpawnController enemySpawner;

    private void OnTriggerEnter(Collider collider)
    {
        enemySpawner.OnEnemyBeyondScreen(collider.GetComponentInParent<IEnemy>());
    }
}
