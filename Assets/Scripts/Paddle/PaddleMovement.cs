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
        if (CanMove())
        {
            rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
        }
    }

    private bool CanMove()
    {
        if (moveDirection == Vector3.zero) return false;
        if (GameManager.Instance.GetPauseGame()) return false;

        return true;
    }
}
