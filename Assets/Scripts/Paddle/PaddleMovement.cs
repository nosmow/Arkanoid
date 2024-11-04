using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float limitLeft, limitRight;

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
            Vector3 newPosition = rb.position + moveDirection * Time.fixedDeltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, limitLeft, limitRight);

            rb.MovePosition(newPosition);
        }
    }

    private bool CanMove()
    {
        if (moveDirection == Vector3.zero) return false;
        if (GameManager.Instance.GetPauseGame()) return false;

        return true;
    }
}
