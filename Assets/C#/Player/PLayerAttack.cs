using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PLayerAttack : MonoBehaviour
{
    /// <summary>
    /// 近战攻击相关参数设置
    /// </summary>
    public bool canShortAttack;//能否近战攻击
    public bool isShortAttacking;//是否正在近战攻击
    public float shortAttackDelta;//近战攻击间隔
    public float shortAttackDamageDelta;//近战攻击伤害判定间隔
    private float _countShortAttackDelta;//计数近战攻击间隔
    private float _countShortAttackDamageDelta;//计数近战攻击伤害判定间隔
    /// <summary>
    /// 远程攻击相关参数设置
    /// </summary>
    public int ballistic;//弹道数量
    public bool canFarAttack;//能否远程攻击
    public float farAttackDelta;//远程攻击间隔
    private float _countFarAttackDelta;//计数远程攻击间隔
    public GameObject bulletPrefab;//子弹预制体 
    private Enemy _nearestEnemy;//最近的敌人
    private float countCheckDelta;//获取最近敌人的判定间隔
    
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (countCheckDelta >= 0) countCheckDelta -= Time.deltaTime;
        GetNearestEnemy();
        //计时近战攻击间隔
        if (_countShortAttackDelta > 0) _countShortAttackDelta -= Time.deltaTime;
        //计时近战攻击伤害判定间隔
        if(_countShortAttackDamageDelta > 0) _countShortAttackDamageDelta -= Time.deltaTime;
        else isShortAttacking = false;
        //远程攻击
        if (_countFarAttackDelta > 0) _countFarAttackDelta -= Time.deltaTime;
        AttackFar();
    }

    private void GetNearestEnemy()
    {
        if (EnemyManager.enemyManager.enemies == null) return;
        countCheckDelta = EnemyManager.enemyManager.checkDelta;
        var distanceSmall = float.MaxValue;
        var distanceNow = 0f;
        Enemy enemyNearest = null;
        foreach (var enemy in  EnemyManager.enemyManager.enemies)
        {
            distanceNow = Vector2.Distance(enemy.transform.position, transform.position);
            if (distanceNow > distanceSmall) continue;
            distanceSmall = distanceNow;
            enemyNearest = enemy;
        }
        _nearestEnemy = enemyNearest;
        Debug.Log(_nearestEnemy);
    }

    public void AttackShort(InputAction.CallbackContext callbackContext)
    {
        if (!canShortAttack) return;
        if (!(_countShortAttackDelta <= 0)) return;
        isShortAttacking = true;
        _countShortAttackDamageDelta = shortAttackDamageDelta;
        _countShortAttackDelta = shortAttackDelta;

    }

    private void AttackFar()
    {
        if (UIManager._uiManager.pauseGame) return;//时停状态下不能响应
        if (!canFarAttack) return;
        if (!bulletPrefab) return;
        if (_countFarAttackDelta > 0) return;
        if (!_nearestEnemy) return;//敌人不存在时不能攻击
        //子弹转向最近的敌人的方向
        Instantiate(bulletPrefab, transform.position, Quaternion.FromToRotation(Vector3.up, (_nearestEnemy.transform.position - transform.position).normalized));
        _countFarAttackDelta = farAttackDelta;

    }
}
