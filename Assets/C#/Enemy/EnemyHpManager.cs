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
    public float countAttackDelta;//计数碰撞攻击间隔
    public float attackDelta;//碰撞攻击间隔
    private bool canBeShortAttack;//检测是否处在玩家攻击范围内
    private float beShortAttackDelta;//被玩家近战攻击的最短间隔 
    private PLayerAttack _pLayerAttack;
    private PlayerHpManager _playerHpManager;

    private void Start()
    {
        _playerHpManager = Player.player.GetComponent<PlayerHpManager>();
        _pLayerAttack = Player.player.GetComponent<PLayerAttack>();
    }

    private void Update()
    {
        if (countAttackDelta > 0) countAttackDelta -= Time.deltaTime;
        if (beShortAttackDelta > 0) beShortAttackDelta -= Time.deltaTime;
        ShortAttackByPlayer();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canBeShortAttack = true;
            PlayerHpManager player = other.gameObject.GetComponent<PlayerHpManager>();
            NearAttackByPlayer(player);
        }
        else canBeShortAttack = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canBeShortAttack = true;
            PlayerHpManager player = other.gameObject.GetComponent<PlayerHpManager>();
            NearAttackByPlayer(player);
        }
        else canBeShortAttack = false;
    }
    
    //玩家碰撞攻击怪物
    private void NearAttackByPlayer(PlayerHpManager player)
    {
        //满足攻击间隔则攻击
        if (player.countAttackDelta <= 0)
        {
            hp -= player.damage;
            player.countAttackDelta = player.attackDelta;
        }

        if (hp <= 0) Destroy(gameObject, 0.2f);
    }

    //玩家近战攻击怪物
    private void ShortAttackByPlayer()
    {
        if (beShortAttackDelta > 0) return;
        if (!_pLayerAttack) return;
        if(!_playerHpManager) return;
        if (_pLayerAttack.isShortAttacking && canBeShortAttack)
        {
            beShortAttackDelta = 0.2f;
            hp -= _playerHpManager.damage;
            if(hp <= 0) Destroy(gameObject, 0.2f);
        }
    }
    
}
