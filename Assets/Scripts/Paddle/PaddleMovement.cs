using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody rb;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector3(movX, 0, 0).normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            //Vector3 newPostion = rb.
            rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
        }
    }
}
