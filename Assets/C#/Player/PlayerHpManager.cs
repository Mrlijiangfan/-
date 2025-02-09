using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHpManager : MonoBehaviour
{
    [SerializeField] private float maxHp;//最大生命值
    private float  hp;//血量
    public float damage;//伤害
    
    public Text hpText;//血量文本

    private void Start()
    {
        hp = maxHp;
        if (hpText != null) hpText.text = hp.ToString() + '/' + maxHp;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //近战怪物攻击玩家
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHpManager enemy = other.gameObject.GetComponent<EnemyHpManager>();
            AttackByEnemy(enemy);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //近战怪物攻击玩家
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHpManager enemy = other.gameObject.GetComponent<EnemyHpManager>();
            AttackByEnemy(enemy);
        }
    }
    
    //近战怪物攻击玩家
    private void AttackByEnemy(EnemyHpManager enemy)
    {
        //满足攻击间隔则攻击
        if (enemy.countAttackDelta <= 0)
        {
            Debug.Log("EnemyAttackStay");
            hp -= enemy.damage;
            enemy.countAttackDelta = enemy.attackDelta;
        }
        if(hp <= 0) 
        {
            Destroy(gameObject, 0.2f);
            SceneManager.LoadScene("Defeat");
        }
        //刷新血量信息
        if (hpText != null) hpText.text = hp.ToString() + '/' + maxHp; 
    }
}
