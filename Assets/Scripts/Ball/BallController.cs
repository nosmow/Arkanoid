using UnityEngine;

public class BallController : MonoBehaviour
{ 
    [Header("Damage Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float offsetSpawnY;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        FindAnyObjectByType<LevelManager>().OnChangedLevel += ResetValues;
        GamePlayManager.Instance.OnBlocksAreGone += ResetValues;
    }

    #region Methods

    private void EnterTheLimit()
    {
        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.TakeDamage(damage);

            ResetValues();
        }
    }

    public void ResetValues()
    {
       Transform paddle = GameObject.FindWithTag("Paddle").transform;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.transform.parent = paddle;
            rb.transform.position = new Vector3(paddle.position.x, -offsetSpawnY, paddle.position.z);
            GamePlayManager.Instance.isBallMoving = false;
        }
    }

    public void IncreaseSize(float size)
    {
        transform.localScale = new Vector3(transform.localScale.x + size, transform.localScale.y + size, transform.localScale.z + size);
    }

    #endregion

    // Increases the speed of the ball every time it collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EnterTheLimit();

            // Apply the sound effect
            AudioManager.Instance.PlayBallLostSound();
        }
        else
        {
            // Apply the sound effect
            AudioManager.Instance.PlayBallHitSound();
        }
    }
}