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
    }

    #region Methods

    private void EnterTheLimit()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(damage);

            ResetValues();
        }
    }

    public void ResetValues()
    {
        Transform playerTransform = GameObject.FindWithTag("Player").transform;

        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.transform.parent = playerTransform;
        rb.transform.position = new Vector3(playerTransform.position.x, -offsetSpawnY, playerTransform.position.z);
        GameManager.Instance.isMoving = false;
    }

    #endregion

    // Increases the speed of the ball every time it collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EnterTheLimit();
        }
    }
}