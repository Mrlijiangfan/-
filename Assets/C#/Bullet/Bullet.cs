using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;//子弹飞行速度

    private void Update()
    {
        Move();
    }

    //子弹移动
    private void Move()
    {
        transform.Translate(Vector2.up * (moveSpeed * Time.deltaTime));
    }
    
}
