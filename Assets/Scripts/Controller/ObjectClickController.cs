using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ObjectClickController : MonoBehaviour, IPointerClickHandler
{
    IEnemy enemy;
    void Start()
    {
        enemy = GetComponent<IEnemy>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        enemy.OnEnemyHit();
    }
}
