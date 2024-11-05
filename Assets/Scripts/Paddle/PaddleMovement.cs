using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float limitLeft, limitRight;

    private Rigidbody rb;
    private Vector3 moveDirection;

    // Input System
    private InputAction move;

    private void Start()
    {
        AssingActionMove();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float movX = move.ReadValue<Vector2>().x;
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

    #region Inputs

    private void AssingActionMove()
    {
        move = InputSystem.actions.FindAction("Move");
        move.performed += InputMove;
        move.canceled -= InputMove;
    }

    private void InputMove(InputAction.CallbackContext ctx)
    {
        move = ctx.action;
    }

    #endregion

    private bool CanMove()
    {
        if (moveDirection == Vector3.zero) return false;
        if (GameManager.Instance.GetPauseGame()) return false;

        return true;
    }
}