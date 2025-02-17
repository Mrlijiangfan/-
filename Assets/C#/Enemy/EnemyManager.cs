using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies;//敌人对象池
    public static EnemyManager enemyManager;//敌人对象池单例
    public float checkDelta;//敌人列表更新间隔
    private float _countCheckDelta;//更新间隔计时器

    private void Awake()
    {
        //单例控制 
        if (enemyManager == null) enemyManager = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (_countCheckDelta >= 0) _countCheckDelta -= Time.deltaTime;
        GetEnemies();
    }

    private void GetEnemies()
    {
        if (_countCheckDelta >= 0) return;
        foreach (var enemy in GetComponentsInChildren<Enemy>())
        {
            if(enemies.Contains(enemy)) continue;
            enemies.Add(enemy);
        }
        _countCheckDelta = checkDelta;
    }
}
