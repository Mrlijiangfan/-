using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float controllX;//水平限定
    [SerializeField] private float controllY;//竖直限定

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (!player) return;
        var cameraPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        if (Mathf.Abs(cameraPos.x) - controllX >= 0)
            cameraPos.x = controllX * Mathf.Sign(transform.position.x);
        if(Mathf.Abs(cameraPos.y) - controllY >= 0)
            cameraPos.y = controllY * Mathf.Sign(transform.position.y);
        transform.position = cameraPos;
    }
}
