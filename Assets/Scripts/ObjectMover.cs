using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveSpeed = 2;
        moveSpeed -= moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(-moveSpeed, 0);
    }
}
