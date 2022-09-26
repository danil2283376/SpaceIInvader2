using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [SerializeField] private TypeInvader _typeInvader = TypeInvader.Default;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _scoreForKill;
    [SerializeField] private float _distanceRay = 500f;

    [HideInInspector] public Invaders Invaders;
    [HideInInspector] public GameObject GameObjectInvader;
    [HideInInspector] public GameObject ParrentTrash;
    [HideInInspector] public float SpeedBullet = 2f;

    private readonly int _layerBunker = 6;
    private readonly int _layerEnemyPlayer = 7;
    private readonly int _layerEnemyBullet = 8;

    private void Start()
    {
        GameObjectInvader = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layerEnemyBullet)
        {
            InvaiderDie();
            Destroy(collision.gameObject);
        }
    }

    public bool FindRayCastPlayer() 
    {
        RaycastHit2D hit = Physics2D.Raycast(_weapon.transform.position, Vector2.down, _distanceRay);
        if (hit.collider.gameObject.layer == _layerEnemyPlayer)
            return true;

        return false;
    }

    public bool FindRayCastBunker() 
    {
        RaycastHit2D hit = Physics2D.Raycast(_weapon.transform.position, Vector2.down, _distanceRay);
        if (hit.collider.gameObject.layer == _layerBunker)
            return true;
        return false;
    }

    public void Shoot() 
    {
        Vector3 newPosition = new Vector3(_weapon.transform.position.x, _weapon.transform.position.y, ParrentTrash.transform.position.z);
        GameObject bullet = Instantiate(_bullet, newPosition, Quaternion.identity, ParrentTrash.transform);
    }

    private void InvaiderDie()
    {
        ScoreHandler.ChangeScore((int)_typeInvader, _scoreForKill);
        Invaders.CountKill++;
        Invaders.CountAlive--;
        gameObject.SetActive(false);
    }
}

public enum TypeInvader 
{
    Invader01,
    Invader02,
    Invader03,
    Invader04,
    Default
};