using UnityEngine;
using Zenject;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public int LoseLimit;
    public float EnemySpawnInterval;
    public Vector2 EnemySpawnRange;
}