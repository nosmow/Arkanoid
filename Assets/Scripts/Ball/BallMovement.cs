using UnityEngine;
using UnityEngine.EventSystems;

public class BallMovement : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float ballSpeed = 10f;
    [SerializeField] private Vector2 initialDirection = new Vector2(0f, -1f);

    private Vector2 currentDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GamePlayManager.Instance.isBallMoving = false;
        currentDirection = initialDirection.normalized;
    }

    private void Update()
    {
        if (CanStart())
        {
            StartMoving();
        }
    }

    private void FixedUpdate()
    {
        if (GamePlayManager.Instance.isBallMoving)
        {
            MoveBall();
        }
    }

    #region Methods

    private bool CanStart()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return false;
        if (GamePlayManager.Instance.isBallMoving) return false;
        if (GameManager.Instance.GetPauseGame()) return false;

        return true;
    }

    // Method for starting the ball movement
    private void StartMoving()
    {
        rb.isKinematic = false;
        GamePlayManager.Instance.isBallMoving = true;
    }

    private void MoveBall()
    {
        rb.linearVelocity = currentDirection * ballSpeed;
    }

    private void ReflectDirection(Collision collision)
    {
        Vector3 contactNormal = collision.contacts[0].normal;
        currentDirection = Vector2.Reflect(currentDirection, contactNormal).normalized;
    }

    private void AdjustDirectionOnPaddle(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 paddleCenter = collision.gameObject.transform.position;

        float hitFactor = (contact.point.x - paddleCenter.x) / collision.collider.bounds.size.x;
        currentDirection = new Vector2(hitFactor, 1).normalized;
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            AdjustDirectionOnPaddle(collision);
        }
        else
        {
            ReflectDirection(collision);
        }
    }
}
