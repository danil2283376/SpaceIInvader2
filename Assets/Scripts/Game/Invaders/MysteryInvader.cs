using UnityEngine;

public class MysteryInvader : MonoBehaviour
{
    [SerializeField] private float _speedInvader;
    [SerializeField] private float _timeToSpawn = 30f;

    private Vector2 direction = Vector2.left;

    private float _tempTime = 0;
    private bool _spawnMystery = false;
    private void Update()
    {
        if (_tempTime >= _timeToSpawn)
        {
            if (_spawnMystery == false)
                SpawnMystery();

        }
        else 
        {
            _tempTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bound")
            DieMystery();

    }

    private void SpawnMystery() 
    {
        _spawnMystery = true;
    }

    private void MoveMystery()
    {
        transform.position = new Vector2(
            transform.position.x + direction.x * _speedInvader * Time.deltaTime,
                transform.position.y);
    }

    private void DieMystery() 
    {
        _spawnMystery = false;
        _tempTime = 0f;
    }
}