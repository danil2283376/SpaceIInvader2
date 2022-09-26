using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBullet;

    public GameObject PrefabBullet
    {
        get 
        {
            if (_prefabBullet != null)
                return _prefabBullet;
            else
                return null;
        }
        private set
        {
            _prefabBullet = value; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bound")
            Destroy(gameObject);
        if (collision.gameObject.tag == "Bullet")
            Destroy(gameObject);
    }
}
