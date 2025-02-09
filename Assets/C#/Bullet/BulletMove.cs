using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float moveSpeed;//子弹飞行速度
    public Vector2 direction;//发射方向

    private void Update()
    {
        Move();
    }

    //子弹移动
    private void Move()
    {
        transform.Translate(direction * (moveSpeed * Time.deltaTime));
    }
    
}
