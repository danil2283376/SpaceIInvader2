using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float _speedShoot = 2f;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private GameObject _parrentForBullet;

    private bool _activeShooting = true;

    private void Start()
    {
        PausedHandler.OnPauseGame += ShootActivate;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0)
            && _activeShooting)
            Shoot();
    }

    private void Shoot() 
    {
        Vector3 newPosition = new Vector3(this.transform.position.x, this.transform.position.y, _parrentForBullet.transform.position.z);
        GameObject bullet = Instantiate(_bullet.PrefabBullet, newPosition, Quaternion.identity, _parrentForBullet.transform);
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _speedShoot, ForceMode2D.Impulse);
    }

    private void ShootActivate()
    {
        _activeShooting = !_activeShooting;
    }
}
