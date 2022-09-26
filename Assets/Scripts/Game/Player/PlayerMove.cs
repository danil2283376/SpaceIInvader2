using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _sizePlayer = 1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            MoveLeft();
        if (Input.GetKey(KeyCode.D))
            MoveRight();
    }

    private void MoveLeft() 
    {
        Vector3 rightBorder = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector3 leftBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);

        float newPositionX = Mathf.Clamp(
            transform.position.x + (Vector2.left.x * _speed * Time.deltaTime),
                leftBorder.x + _sizePlayer,
                    rightBorder.x);

        this.transform.position = new Vector3(
            newPositionX,
                this.transform.position.y,
                    this.transform.position.z);
    }

    private void MoveRight() 
    {
        Vector3 rightBorder = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector3 leftBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);

        float newPositionX = Mathf.Clamp(
            transform.position.x + (Vector2.right.x * _speed * Time.deltaTime),
                leftBorder.x,
                    rightBorder.x - _sizePlayer);
        
        this.transform.position = new Vector3(
            newPositionX,
                this.transform.position.y,
                    this.transform.position.z);
    }
}
