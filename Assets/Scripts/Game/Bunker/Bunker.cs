using UnityEngine;

public class Bunker : MonoBehaviour
{
    private readonly int _layerInvader = 3;
    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.AddComponent<Bunker>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("KEK");
        if (collision.gameObject.tag == "Bullet")
        {
            DestroyBunker();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == _layerInvader)
        {
            DestroyBunker();
        }
    }

    private void DestroyBunker()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        if (gameObject.GetComponent<BoxCollider2D>() != null)
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
