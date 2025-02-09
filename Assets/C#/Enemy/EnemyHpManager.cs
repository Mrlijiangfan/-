using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyHpManager : MonoBehaviour
{
    [SerializeField] private float  hp;//血量
    public float damage;//伤害
    public float countAttackDelta;//计数攻击间隔
    public float attackDelta;//攻击间隔
       
    private void Update()
    {
        if(countAttackDelta > 0)
            countAttackDelta -= Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //怪物被玩家碰撞攻击
        if (other.gameObject.CompareTag("Player"))
        {
            hp -= other.gameObject.GetComponent<PlayerHpManager>().damage;
            if(hp <= 0) Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            
        }
    }
    

}
