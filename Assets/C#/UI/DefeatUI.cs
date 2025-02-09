using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatUI : MonoBehaviour
{
    public PlayerInput PlayerInput;
    private void Awake()
    {
        PlayerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        PlayerInput.Player.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Player.Disable();
    }

    private void Update()
    {
        if (PlayerInput.Player.Choose.IsPressed()) SceneManager.LoadScene("Game");
    }
}
