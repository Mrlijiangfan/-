using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed;//敌人移动速度 
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(Player.player)
        {
            if (Vector2.Distance(Player.player.transform.position, transform.position) >= 0.2f)
            { 
                Vector2 direction = Vector2.MoveTowards(transform.position, Player.player.transform.position, Time.deltaTime * speed);
                //转向
                if (direction.x - transform.position.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
                else transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position = direction;
            }
        }
    }
}
