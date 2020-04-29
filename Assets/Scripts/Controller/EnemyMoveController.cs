using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController
{
    private bool isLeft;
    private bool definedSide;
    private float timer;
    Transform enemyTransform;
    float speed;
    bool moveToSides;
    float sideMoveInterval;
    float moveInterval;
    public EnemyMoveController(Transform _enemyTransform, float _speed, bool _moveToSides, float _sideMoveInterval = 0, float _moveInterval = 0)
    {
        enemyTransform = _enemyTransform;
        speed = _speed;
        moveToSides = _moveToSides;
        sideMoveInterval = _sideMoveInterval;
        moveInterval = _moveInterval;
    }
    public void Move()
    {
        if (moveToSides && CanMoveSide())
            MoveRandomSide();
        else
            MoveDown();

    }
    private void MoveRandomSide()
    {
        if (!definedSide)
        {
            isLeft = Random.Range(0, 2) == 0;
            definedSide = true;
        }
        MoveSide();


    }

    private void MoveDown()
    {
        enemyTransform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void MoveSide()
    {
        Debug.Log(OnScreenEdge());
        if (!OnScreenEdge())
        {
            Vector3 side = isLeft ? Vector3.left : Vector3.right;
            enemyTransform.Translate((Vector3.down + side) * speed * Time.deltaTime);
        }
        else
        {
            MoveDown();
        }
    }
    
    private bool CanMoveSide()
    {
        if (timer < sideMoveInterval)
        {
            timer += Time.deltaTime;
            return true;
        }
        else if (timer < moveInterval)
        {
            definedSide = false;
            timer += Time.deltaTime;
            return false;
        }
        timer = 0;
        return false;
    }

    private bool OnScreenEdge()
    {
        if (enemyTransform.position.x < ScreenHelper.ScreenBounds().x && enemyTransform.position.x > -ScreenHelper.ScreenBounds().x)
            return false;
        return true;
    }

}
