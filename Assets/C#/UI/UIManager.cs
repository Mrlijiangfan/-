using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private int _currentUIIndex;//当前选中的UI画布的索引
    public static bool PauseGame;//是否暂停游戏窗口
    public static bool KillGame;//是否结束游戏窗口
    
    private PlayerInput _playerInput;
    [SerializeField]private PLayerAttack playerAttack;
    public static UIManager _uiManager;//单例模式
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Choose.started += ChangeUIByJ;
        _playerInput.Player.Exit.started += ChangeUIByEsc;
        _playerInput.Player.ChangeUIByK.started += ChangeUIByK;
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Choose.started -= ChangeUIByJ;
        _playerInput.Player.Exit.started -= ChangeUIByEsc;
        _playerInput.Player.ChangeUIByK.started -= ChangeUIByK;
        _playerInput.Player.Disable();
    }

    private void Start()
    {
        //控制单例
        if(_uiManager == null) _uiManager = this;
        else Destroy(gameObject);
        
        if (gameObject)
        {
            _currentUIIndex = 0;
            gameObject.transform.GetChild(_currentUIIndex).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        ControllPlayerAttack();
        ControllPause();
        ControllKill();
    }

    private void ControllPlayerAttack()
    {
        //监听近战攻击
        if(_currentUIIndex == 2) _playerInput.Player.Choose.started += playerAttack.AttackShort;
        else _playerInput.Player.Choose.started -= playerAttack.AttackShort;
    }

    private void ControllPause()
    {
        if (_currentUIIndex != 2)
        {
            PauseGame = true;
            Time.timeScale = 0;//时停
        }
        else
        {
            PauseGame = false;
            Time.timeScale = 1;//关闭时停
        }
    }
    
    private void ControllKill()
    {
        KillGame = (_currentUIIndex < 2);
    }

    private void ChangeUIByJ(InputAction.CallbackContext callbackContext)
    {
        gameObject.transform.GetChild(_currentUIIndex).gameObject.SetActive(false);

        switch (_currentUIIndex)
        {
            case 0:
                _currentUIIndex = 1;
                break;
            case 1:
                _currentUIIndex = 2;
                break;
            case 3:
                _currentUIIndex = 4;
                break;
            case 4:
                _currentUIIndex = 5;
                break;
            case 5:
                _currentUIIndex = 3;
                break;
        }
        
        gameObject.transform.GetChild(_currentUIIndex).gameObject.SetActive(true);
    }
    
    private void ChangeUIByK(InputAction.CallbackContext callbackContext)
    {
        gameObject.transform.GetChild(_currentUIIndex).gameObject.SetActive(false);

        switch (_currentUIIndex)
        {
            case 2:
                _currentUIIndex = 3;
                break;
            case 3:
                _currentUIIndex = 2;
                break;
            case 4:
                _currentUIIndex = 2;
                break;
            case 5:
                _currentUIIndex = 2;
                break;
        }
        
        gameObject.transform.GetChild(_currentUIIndex).gameObject.SetActive(true);
    }
    
    private void ChangeUIByEsc(InputAction.CallbackContext callbackContext)
    {
        gameObject.transform.GetChild(_currentUIIndex).gameObject.SetActive(false);

        switch (_currentUIIndex)
        {
            case 0:
                break;
            case 1:
                _currentUIIndex = 0;
                break;
            case 2:
                _currentUIIndex = 1;
                break;
            case 3:
                _currentUIIndex = 2;
                break;
            case 4:
                _currentUIIndex = 2;
                break;
            case 5:
                _currentUIIndex = 2;
                break;
        }
        
        gameObject.transform.GetChild(_currentUIIndex).gameObject.SetActive(true);
    }
    
}
