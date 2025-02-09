using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLayerAttack : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void AttackShort(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("近战攻击");
    }

    public void AttackFar(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("远程攻击");
    }
}
