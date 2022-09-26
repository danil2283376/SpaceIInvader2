using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public delegate void DiePlayerHandler();
    public static event DiePlayerHandler DiePlayer;

    [SerializeField] private HealthBar _healthPointUI;

    private int _currentHP = 3;

    private readonly int _layerEnemyBullet = 9;
    private readonly int _layerInvader = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layerEnemyBullet)
        {
            Destroy(collision.gameObject);
            CheckStatePlayer();
        }
        if (collision.gameObject.layer == _layerInvader)
            CheckStatePlayer();
    }

    private void CheckStatePlayer()
    {
        //Сделать UI
        _currentHP--;
        if (_currentHP <= 0)
            PlayerDie();
        else
            HitHandlerPlayer();
    }

    private void PlayerDie() 
    {
        _currentHP = 0;
        HitHandlerPlayer();
        DiePlayer();
    }

    private void HitHandlerPlayer()
    {
        _healthPointUI.ChangeHealthBar(_currentHP);
    }
}
