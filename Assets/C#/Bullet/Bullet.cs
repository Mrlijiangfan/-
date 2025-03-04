using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;//子弹飞行速度
    public float suriveTime;//最大飞行时间
    private float _countSuriveTime;//计时当前飞行时限

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        _countSuriveTime = suriveTime;
    }

    private void OnDisable()
    {
        //归还对象时对一些数值做还原
        _countSuriveTime = suriveTime;
    }

    private void Update()
    {
        Move();
        if (_countSuriveTime <= 0)
        {
            _countSuriveTime = suriveTime;
            CharacterPool.characterPool.ReturnObject(gameObject);
        }
        else _countSuriveTime -= Time.deltaTime;
    }

    //子弹移动
    private void Move()
    {
        transform.Translate(Vector2.up * (moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Player")) CharacterPool.characterPool.ReturnObject(gameObject);
        if (other.CompareTag("EnemyReturnBullet")) CharacterPool.characterPool.ReturnObject(gameObject);
    }
}
