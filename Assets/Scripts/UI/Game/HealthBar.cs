using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public void ChangeHealthBar(int _currentHP)
    {
        Transform spriteHP = transform.GetChild(_currentHP);
        spriteHP.gameObject.SetActive(false);
    }
}