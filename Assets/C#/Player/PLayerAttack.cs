using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLayerAttack : MonoBehaviour
{
    public int ballistic;//弹道数量
    public bool canShortAttack;//能否近战攻击
    public bool canFarAttack;//能否远程攻击
    public bool isShortAttacking;//是否正在近战攻击
    public float shortAttackDelta;//近战攻击间隔
    public float ShortAttackDamageDelta;//近战攻击伤害判定间隔
    private float countShortAttackDelta;//计数近战攻击间隔
    private float countShortAttackDamageDelta;//计数近战攻击伤害判定间隔
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (countShortAttackDelta > 0)
            countShortAttackDelta -= Time.deltaTime;
        if(countShortAttackDamageDelta > 0) countShortAttackDamageDelta -= Time.deltaTime;
        else isShortAttacking = false;
    }

    public void AttackShort(InputAction.CallbackContext callbackContext)
    {
        if (countShortAttackDelta <= 0)
        {
            Debug.Log("近战攻击");
            isShortAttacking = true;
            countShortAttackDamageDelta = ShortAttackDamageDelta;
            countShortAttackDelta = shortAttackDelta;
        }
        
    }

    public void AttackFar(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("远程攻击");
    }
}
