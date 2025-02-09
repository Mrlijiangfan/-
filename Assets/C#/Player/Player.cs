using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float moveSpeed;//速度
    
    public PlayerInput playerInput;
    public static Player player;//玩家单例

    private void Awake()
    {
        playerInput = new PlayerInput();
        //控制单例
        if(player == null) player = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        playerInput.Player.RefreshByMouse.started += RefreshStoreByMouse; 
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.RefreshByMouse.started -= RefreshStoreByMouse; 
        playerInput.Player.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //玩家到达商店刷新按钮检测范围后监听按键
        if (other.name == "RefreshButton")
            playerInput.Player.RefreshByJ.started += RefreshStoreByJ;
        //玩家到达战斗开始按钮检测范围后监听按键
        if (other.name == "StartFightButton")
            playerInput.Player.RefreshByJ.started += StartFight;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //玩家离开商店刷新按钮检测范围后撤销监听按键
        if (other.name == "RefreshButton")
            playerInput.Player.RefreshByJ.started -= RefreshStoreByJ;
        //玩家离开战斗开始按钮检测范围后撤销监听按键
        if (other.name == "StartFightButton")
            playerInput.Player.RefreshByJ.started -= StartFight;
    }
    
    private void Move()
    {
        Vector2 moveValue = playerInput.Player.WASD.ReadValue<Vector2>();
        if (moveValue.x > 0)
        {
            moveValue.x = -moveValue.x;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(moveValue.x < 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Translate(moveValue * (Time.deltaTime * moveSpeed));
    }

    //鼠标点击刷新商店
    private void RefreshStoreByMouse(InputAction.CallbackContext callbackContext)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            if (hit.collider.name == "RefreshButton")
            {
                Debug.Log("Refresh");
            }
        }
    }
    //角色靠近商店刷新按钮点击刷新
    private void RefreshStoreByJ(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Refresh");
    }
    //角色靠近战斗开始按钮点击开始战斗
    private void StartFight(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("StartFight");
    }
    //角色近战攻击
    
}
